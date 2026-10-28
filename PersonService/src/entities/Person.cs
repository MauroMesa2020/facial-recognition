namespace PersonService.Entities;

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string DocumentNumber { get; set; }

    public DateTime BirthDate { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public List<PersonEmbedding> Embeddings { get; set; } = [];
}