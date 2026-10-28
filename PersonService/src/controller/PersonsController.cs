using Microsoft.AspNetCore.Mvc;
using PersonService.dtos;
using PersonService.Service;

namespace PersonService.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    private readonly PersonAppService _personService;

    public PersonController(PersonAppService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<CreatedPersonResponse>> AddPerson(
        [FromForm] CreatePersonRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _personService.AddPerson(
            request,
            cancellationToken);

        // to return the 201 status code
        return Created($"/api/person/{response.Id}", response);
    }

    [HttpGet("{id:guid}/embeddings")]
    public async Task<ActionResult<PersonEmbeddingsResponse>> GetEmbeddings(
            Guid id,
            CancellationToken cancellationToken
        )
    {
        var response = await _personService.GetEmbeddingsAsync(
            id, cancellationToken);

        if (response is null)
            return NotFound();

        return Ok(response);
    }
}