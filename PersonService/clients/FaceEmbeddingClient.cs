using System.Net.Http.Json;
using PersonService.dtos;

namespace PersonService.Clients;

public class FaceEmbeddingClient
{
    private readonly HttpClient _httpClient;

    public FaceEmbeddingClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<float[]> GenerateEmbeddingAsync(
        // Stream is a stream of bytes. In this case, it's to represent an image.
        Stream photo,
        string fileName,
        CancellationToken cancellationToken)
    {
        using var form = new MultipartFormDataContent();

        form.Add(
            new StreamContent(photo),
            "image",
            fileName
        );

        using var response = await _httpClient.PostAsync(
            "api/embedding",
            form,
            cancellationToken
        );

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<EmbeddingResponse>(
                cancellationToken: cancellationToken
            );

        return result?.Embedding
            ?? throw new InvalidOperationException(
                "FaceEmbedding no devolvió un embedding."
            );
    }
}