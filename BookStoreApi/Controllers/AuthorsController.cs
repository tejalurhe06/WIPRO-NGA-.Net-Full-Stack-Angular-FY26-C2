using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;
using BookStoreApi.Data;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly BookStoreContext _context;

    public AuthorsController(BookStoreContext context)
    {
        _context = context;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound(new { message = $"Author with ID {id} not found" });
        }

        return author;
    }

    // GET: api/Authors/5/Books
    [HttpGet("{authorId}/books")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int authorId)
    {
        var author = await _context.Authors.FindAsync(authorId);
        if (author == null)
        {
            return NotFound(new { message = $"Author with ID {authorId} not found" });
        }

        return await _context.Books
            .Where(b => b.AuthorId == authorId)
            .ToListAsync();
    }

    // POST: api/Authors
    [HttpPost]
    public async Task<ActionResult<Author>> PostAuthor(Author author)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    // PUT: api/Authors/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, Author author)
    {
        if (id != author.Id)
        {
            return BadRequest(new { message = "ID in URL does not match ID in body" });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(id))
            {
                return NotFound(new { message = $"Author with ID {id} not found" });
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound(new { message = $"Author with ID {id} not found" });
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AuthorExists(int id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }
}