using FaceEmbedding.dto;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace FaceEmbedding.Service
{
    public class EmbeddingService
    {
        private readonly InferenceSession _session;

 public EmbeddingService(IConfiguration configuration)
        {
            var modelPath = configuration["EmbeddingModel:Path"];

            if (string.IsNullOrWhiteSpace(modelPath))
            {
                throw new InvalidOperationException(
                    "EmbeddingModel:Path no está configurado."
                );
            }

            var fullPath = Path.GetFullPath(modelPath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(
                    $"No se encontró el modelo: {fullPath}"
                );
            }

            _session = new InferenceSession(fullPath);

            Console.WriteLine("Embedding model loaded.");
        }


        public EmbeddingResponse generateEmbedding(byte[] imageBytes)
        {
            if(imageBytes.Length == 0)
            {
                throw new ArgumentException(
                    "Image is empty",
                    nameof(imageBytes)
                );
            }

            using var image = Image.Load<Rgb24>(imageBytes);

            image.Mutate(x =>
                x.Resize(new ResizeOptions
                {
                    Size = new Size(112, 112),
                    Mode = ResizeMode.Stretch
                })
            );

            var input = new DenseTensor<float>(
                new[] { 1, 3, 112, 112 }
            );

            for (int y = 0; y < 112; y++)
            {
                for (int x = 0; x < 112; x++)
                {
                    var pixel = image[x, y];

                    input[0, 0, y, x] = Normalize(pixel.R);
                    input[0, 1, y, x] = Normalize(pixel.G);
                    input[0, 2, y, x] = Normalize(pixel.B);
                }
            }

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor(
                    "input.1",
                    input
                )
            };

            using var results = _session.Run(inputs);

            var output = results.First(
                x => x.Name == "683"
            );

            var embedding = output
                .AsEnumerable<float>()
                .ToArray();

            EmbeddingResponse response = new EmbeddingResponse();
            response.Embedding = embedding;

            return response;
        }

        private static float Normalize(byte value)
        {
            return (value - 127.5f) / 127.5f;
        }
    }
}