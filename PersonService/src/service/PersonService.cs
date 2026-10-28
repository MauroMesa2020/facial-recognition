using PersonService.Clients;
using PersonService.dtos;
using PersonService.Entities;
using PersonService.Repository;

namespace PersonService.Service{

    public class PersonAppService
    {
        private readonly IPersonRepository _personRepository;
        private readonly FaceEmbeddingClient _faceEmbeddingClient;

        public PersonAppService(
            IPersonRepository personRepository,
            FaceEmbeddingClient faceEmbeddingClient)
        {
            _personRepository = personRepository;
            _faceEmbeddingClient = faceEmbeddingClient;
        }

        public async Task<CreatedPersonResponse> AddPerson(
            CreatePersonRequest request,
            CancellationToken cancellationToken)
        {
            if (request.Photos.Count == 0)
            {
                throw new ArgumentException(
                    "You need to upload at least one photo.");
            }

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DocumentNumber = request.DocumentNumber,
                BirthDate = request.BirthDate,
                Email = request.Email,
                Phone = request.Phone
            };

            foreach (var photo in request.Photos)
            {
                if (photo.Length == 0)
                {
                    throw new ArgumentException(
                        $"The photo {photo.FileName} is empty.");
                }

                await using var stream = photo.OpenReadStream();

                var vector = await _faceEmbeddingClient
                    .GenerateEmbeddingAsync(
                        stream,
                        photo.FileName,
                        cancellationToken);

                person.Embeddings.Add(new PersonEmbedding
                {
                    PersonId = person.Id,
                    Vector = vector
                });
            }

            await _personRepository.AddAsync(person);
            await _personRepository.SaveChangesAsync();

            return new CreatedPersonResponse
            {
                Id = person.Id,
                Name = person.FirstName,
                LastName = person.LastName,
                EmbeddingsCount = person.Embeddings.Count
            };
        }

        public async Task<PersonEmbeddingsResponse?> GetEmbeddingsAsync(
            Guid personId,
            CancellationToken cancellationToken)
        {
            var embeddings = await _personRepository.GetEmbeddingsByPersonIdAsync(personId, cancellationToken);

            if (embeddings.Count == 0)
                return null;

            return new PersonEmbeddingsResponse
            {
                Id = personId,
                Embeddings = embeddings.Select(e => new PersonEmbeddingdto
                {
                    Id = e.Id,
                    CreatedAt = e.CreatedAt,
                    Vector = e.Vector
                }).ToList()
            };
        }
    }
}