namespace PersonService.dtos
{
    public class PersonEmbeddingdto
    {
            public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public float[] Vector { get; set; } = [];
    }
}