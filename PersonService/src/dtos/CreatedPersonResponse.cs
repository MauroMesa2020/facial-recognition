namespace PersonService.dtos
{
    public class CreatedPersonResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        public int EmbeddingsCount { get; set; }
    }
}