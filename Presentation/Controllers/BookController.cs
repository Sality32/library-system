using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

[Authorize]
[Route("/books")]
[ApiController]
public class BookController: ControllerBase
{
    private readonly BookService bookService;
    public BookController(BookService bookService)
    {
        this.bookService = bookService;
    }

    // GET api/v1/books
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        return Ok(await bookService.GetAllBooksAsync());
    }

    // POST api/v1/books
    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
    {
        var book = await bookService.AddBook(createBookDto);
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpPost("borrow-request")]
    public async Task<IActionResult> RequestBorrowBook([FromBody] RequestBorrowDto requestBorrow)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var request = await bookService.AddBorrowedRequestAsync(new CreateBorrowedRequestDto 
        { 
            BookId = requestBorrow.BookId, 
            UserId = Guid.Parse(userId) 
        });
        return StatusCode(201, new { message = request });
    }

    [Authorize(Policy = "Admin")]
    [HttpPost("borrow-request/approval")]
    public async Task<IActionResult> ApprovalRequestBorrow([FromBody] ApprovalBorrowedRequestDto approvalBorrowedRequest)
    {
        var result = await bookService.ApprovalBorrowedRequest(approvalBorrowedRequest);
        return Ok(result);
    }

    [Authorize(Policy = "Officer")]
    [HttpPost("request-new-collection")]
    public async Task<IActionResult> RequestNewCollection([FromBody] string year)
    {
        var result = await bookService.RequestAddNewBookCollection(year);
        return Ok(result);
    }
}