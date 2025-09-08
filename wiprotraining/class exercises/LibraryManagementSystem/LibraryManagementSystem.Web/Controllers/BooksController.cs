using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace LibraryManagementSystem.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BooksController(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetBooksWithAuthorsAndGenresAsync();

            // Add success message if redirected from create/edit/delete
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var authors = await _authorRepository.GetAllAsync();
            var genres = await _genreRepository.GetAllAsync();

            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            return View();
        }


        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.AddAsync(book);
                await _bookRepository.SaveChangesAsync();

                TempData["SuccessMessage"] = "Book created successfully!";
                return RedirectToAction(nameof(Index));
            }

            // If ModelState invalid, repopulate dropdowns
            var authors = await _authorRepository.GetAllAsync();
            var genres = await _genreRepository.GetAllAsync();

            ViewBag.Authors = new SelectList(authors, "Id", "Name", book.AuthorId);
            ViewBag.Genres = new SelectList(genres, "Id", "Name", book.GenreId);

            return View(book);
        }




        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Get authors and genres and convert to SelectListItem
            var authors = await _authorRepository.GetAllAsync();
            var genres = await _genreRepository.GetAllAsync();

            ViewBag.Authors = authors?.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
                Selected = a.Id == book.AuthorId
            }).ToList() ?? new List<SelectListItem>();

            ViewBag.Genres = genres?.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = g.Id == book.GenreId
            }).ToList() ?? new List<SelectListItem>();

            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookRepository.Update(book);
                    await _bookRepository.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Book updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the book: " + ex.Message);
                }
            }

            // Repopulate dropdowns if validation fails
            var authors = await _authorRepository.GetAllAsync();
            var genres = await _genreRepository.GetAllAsync();

            ViewBag.Authors = authors?.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,
                Selected = a.Id == book.AuthorId
            }).ToList() ?? new List<SelectListItem>();

            ViewBag.Genres = genres?.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = g.Id == book.GenreId
            }).ToList() ?? new List<SelectListItem>();

            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetBookWithDetailsAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book != null)
                {
                    _bookRepository.Remove(book);
                    await _bookRepository.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Book deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Book not found!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the book: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // AJAX: Get books by author
        [HttpGet]
        public async Task<JsonResult> GetBooksByAuthor(int authorId)
        {
            try
            {
                var books = await _bookRepository.GetBooksByAuthorAsync(authorId);
                return Json(books.Select(b => new { id = b.Id, title = b.Title }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting books by author: {ex.Message}");
                return Json(new { error = "An error occurred while fetching books." });
            }
        }

        // AJAX: Get books by genre
        [HttpGet]
        public async Task<JsonResult> GetBooksByGenre(int genreId)
        {
            try
            {
                var books = await _bookRepository.GetBooksByGenreAsync(genreId);
                return Json(books.Select(b => new { id = b.Id, title = b.Title }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting books by genre: {ex.Message}");
                return Json(new { error = "An error occurred while fetching books." });
            }
        }

        // Add these methods to your BooksController class

        // AJAX: Get all authors for dropdown
        [HttpGet]
        public async Task<JsonResult> GetAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();
                return Json(authors.Select(a => new { id = a.Id, name = a.Name }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting authors: {ex.Message}");
                return Json(new { error = "An error occurred while fetching authors." });
            }
        }

        // AJAX: Get all genres for dropdown
        [HttpGet]
        public async Task<JsonResult> GetGenres()
        {
            try
            {
                var genres = await _genreRepository.GetAllAsync();
                return Json(genres.Select(g => new { id = g.Id, name = g.Name }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting genres: {ex.Message}");
                return Json(new { error = "An error occurred while fetching genres." });
            }
        }
    }
}