using FaceRecognition.clients;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/recognition")]
public class FaceRecognitionController: ControllerBase
{
    private readonly FaceDetectionClient _faceDetectionClient;

    public FaceRecognitionController(FaceDetectionClient faceDetectionClient)
    {
        _faceDetectionClient = faceDetectionClient;
    }

    [HttpPost]
    public async Task<IActionResult> recognition(IFormFile image, CancellationToken cancellationToken)
    {

        

        var listImages = await _faceDetectionClient.getPhotosAsync(image, cancellationToken);

        return File(listImages[0], "image/jpeg"); 
    }
}