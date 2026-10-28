using PersonService.Entities;

namespace PersonService.Repository
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(Guid id);
        Task<List<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task SaveChangesAsync();

        Task<List<PersonEmbedding>> GetEmbeddingsByPersonIdAsync( Guid personId, CancellationToken cancellationToken);
    }
}