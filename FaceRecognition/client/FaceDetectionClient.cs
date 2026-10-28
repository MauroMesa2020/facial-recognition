namespace FaceRecognition.clients
{
    public class FaceDetectionClient
    {
        private readonly HttpClient _httpClient;

        public FaceDetectionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<byte[]>> getPhotosAsync(IFormFile image,  CancellationToken cancellationToken)
        {

            using var stream = image.OpenReadStream();

            using var form = new MultipartFormDataContent();

            form.Add(
                new StreamContent(stream),
                "image",           // debe coincidir con el nombre del parámetro
                image.FileName
            );

            using var response = await _httpClient.PostAsync(
                    "api/detection",
                    form,
                    cancellationToken
                );

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<List<byte[]>>(
                    cancellationToken: cancellationToken
                );


            if(result == null)
            {
                throw new InvalidOperationException(
                "FaceEmbedding no devolvió un embedding."
            );
            }

            return result;
        }
    }
}