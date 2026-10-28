namespace PersonService.Entities;

public class PersonEmbedding
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PersonId { get; set; }

    public float[] Vector { get; set; } = [];

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}