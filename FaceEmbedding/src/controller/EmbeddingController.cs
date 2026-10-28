using FaceEmbedding.Service;
using Microsoft.AspNetCore.Mvc;

namespace FaceEmbedding.Controller
{
    [ApiController]
    [Route("api/embedding")]
    public class EmbeddingController: ControllerBase
    {
        private readonly EmbeddingService _embeddingService;

        public EmbeddingController(EmbeddingService embeddingService)
        {
            _embeddingService = embeddingService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            Console.WriteLine("CONTROLLER TEST REACHED");

            return Ok("Controller works");
        }

        [HttpPost]
        public async Task<IActionResult> GenerateEmbedding(IFormFile image)
        {    
            
            Console.WriteLine("=================================");
            Console.WriteLine("CONTROLLER REACHED");
            Console.WriteLine("=================================");



            if (image == null || image.Length == 0)
            {
                return BadRequest("You must upload an image");
            }

            Console.WriteLine($"File: {image.FileName}");
            Console.WriteLine($"ContentType: {image.ContentType}");
            Console.WriteLine($"Size: {image.Length} bytes");

            using var memoryStream = new MemoryStream();

            await image.CopyToAsync(memoryStream);

            var response = _embeddingService.generateEmbedding(
                memoryStream.ToArray()
            );

            return Ok(response);
        }


    }
}