using FaceDetection.service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/detection")]
public class FaceDetectionController: ControllerBase
{
    private readonly FaceDetectionService _faceDetectionService;


    public FaceDetectionController(FaceDetectionService faceDetectionService)
    {
        _faceDetectionService = faceDetectionService;
    }

    [HttpPost]
    public async Task<IActionResult> getPhotos (IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest("You must upload an image");
        }

        Console.WriteLine($"File: {image.FileName}");

        var response = _faceDetectionService.runModelToGetPhotos(image);

        if (response.Count == 0)
            return NotFound("No faces detected");

        // esto para cuando envie bytes
        return Ok(response);

        
        // para ver las imagenes en postmans
        // return File(response[2], "image/jpeg"); 

    }


}