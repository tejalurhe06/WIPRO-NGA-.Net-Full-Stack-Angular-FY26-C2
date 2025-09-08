using BookStore.Web.Data;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace BookStore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repo;
        private readonly DisconnectedBookService _dsService;
        private readonly ILogger<BooksController> _logger;
        public BooksController(IBookRepository repo, DisconnectedBookService
        dsService, ILogger<BooksController> logger)
        {
            _repo = repo; _dsService = dsService; _logger = logger;
        }
        // READ – list with SqlDataReader (connected)
        public async Task<IActionResult> Index()
        {
            var books = await _repo.GetAllAsync();
            return View(books);
        }
        // READ – details
        public async Task<IActionResult> Details(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }
        // CREATE – GET
        public IActionResult Create() => View(new Book
        {
            Stock = 0,
            Price =
        0
        });
        // CREATE – POST (parameterized via stored proc)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                var newId = await _repo.AddAsync(model);
                TempData["Message"] = $"Book created with Id {newId}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book");
                ModelState.AddModelError(string.Empty, "An error occurred whilecreating the book.");
                return View(model);
            }
        }
        // EDIT – GET
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }
        // EDIT – POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            try
            {
                await _repo.UpdateAsync(model);
                TempData["Message"] = "Book updated";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the book.");
            return View(model);
            }
        }
        // DELETE – GET
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }
        // DELETE – POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _repo.DeleteAsync(id);
                TempData["Message"] = "Book deleted";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book");
                TempData["Error"] = "An error occurred while deleting thebook.";
            }
            return RedirectToAction(nameof(Index));
        }
        // DISCONNECTED demo – increase all prices by 5% using DataSet/DataTable
        [HttpPost]
        public async Task<IActionResult> IncreasePrices()
        {
            var rows = await _dsService.IncreaseAllPricesAsync(0.05m);
            TempData["Message"] = $"Prices updated for {rows} row(s).";
            return RedirectToAction(nameof(Index));
        }
    }
}