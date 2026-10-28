using OpenCvSharp;

namespace FaceDetection.service
{
    public class FaceDetectionService
    {

        private readonly string _modelPath;

        public FaceDetectionService(IWebHostEnvironment environment)
        {
            _modelPath = Path.Combine(
                environment.ContentRootPath,
                "model",
                "face_detection_yunet_2023mar.onnx"
            );

            if (!File.Exists(_modelPath))
            {
                throw new FileNotFoundException(
                    "YuNet model not found.",
                    _modelPath
                );
            }
        }


        public List<byte[]> runModelToGetPhotos(IFormFile image)
        {
            var res = new List<byte[]>();

            Console.WriteLine("Entro correctamente en run model");

            using var stream = new MemoryStream();
            image.CopyTo(stream);

            using var original = Cv2.ImDecode(
                stream.ToArray(),
                ImreadModes.Color
            );

            if (original.Empty())
                throw new ArgumentException("Invalid image");

            using var detector = FaceDetectorYN.Create(
                _modelPath,
                "",
                new Size(original.Width, original.Height),
                0.8f,
                0.3f,
                5000
            );

            using var faces = new Mat();

            detector.Detect(original, faces);

            for (int i = 0; i < faces.Rows; i++)
            {
                int x = (int)faces.At<float>(i, 0);
                int y = (int)faces.At<float>(i, 1);
                int width = (int)faces.At<float>(i, 2);
                int height = (int)faces.At<float>(i, 3);

                // Evitar coordenadas fuera de la imagen.
                x = Math.Clamp(x, 0, original.Width - 1);
                y = Math.Clamp(y, 0, original.Height - 1);
                width = Math.Min(width, original.Width - x);
                height = Math.Min(height, original.Height - y);

                if (width <= 0 || height <= 0)
                    continue;

                using var face = new Mat(
                    original,
                    new Rect(x, y, width, height)
                );

                Cv2.ImEncode(".jpg", face, out byte[] faceBytes);

                res.Add(faceBytes);
            }

            Console.WriteLine($"Caras detectadas: {res.Count}");

            return res;
        }
    }
}