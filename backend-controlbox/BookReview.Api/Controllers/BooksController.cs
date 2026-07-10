using BookReview.Application.Books.Queries.GetBookById;
using BookReview.Application.Books.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ISender _mediator;

    public BooksController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? searchTerm, [FromQuery] Guid? categoryId)
    {
        var result = await _mediator.Send(new GetBooksQuery(searchTerm, categoryId));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}