using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;
using BookStoreApi.Data;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookStoreContext _context;

    public BooksController(BookStoreContext context)
    {
        _context = context;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.Include(b => b.Author).ToListAsync();
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound(new { message = $"Book with ID {id} not found" });
        }

        return book;
    }

    // POST: api/Books
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        // Check if model validation passed
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate author exists
        var author = await _context.Authors.FindAsync(book.AuthorId);
        if (author == null)
        {
            return BadRequest(new { message = "Author not found" });
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Return the created book with author details
        var createdBook = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == book.Id);

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, createdBook);
    }

    // PUT: api/Books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest(new { message = "ID in URL does not match ID in body" });
        }

        // Check if model validation passed
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validate author exists
        var author = await _context.Authors.FindAsync(book.AuthorId);
        if (author == null)
        {
            return BadRequest(new { message = "Author not found" });
        }

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound(new { message = $"Book with ID {id} not found" });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound(new { message = $"Book with ID {id} not found" });
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}