namespace PersonService.dtos
{
    public class PersonEmbeddingsResponse
    {

        public Guid Id { get; set; }
        public List<PersonEmbeddingdto> Embeddings { get; set; } = [];
    }
}