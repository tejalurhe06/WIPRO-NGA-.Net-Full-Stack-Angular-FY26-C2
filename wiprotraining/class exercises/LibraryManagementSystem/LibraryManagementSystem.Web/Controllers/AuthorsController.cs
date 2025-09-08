using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryManagementSystem.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAuthorsWithBooksAsync();
            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorRepository.GetAuthorWithDetailsAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepository.AddAsync(author);
                await _authorRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _authorRepository.Update(author);
                await _authorRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepository.GetAuthorWithDetailsAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author != null)
            {
                _authorRepository.Remove(author);
                await _authorRepository.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // AJAX: Get author details
        [HttpGet]
        public async Task<JsonResult> GetAuthorDetails(int id)
        {
            try
            {
                var author = await _authorRepository.GetAuthorWithDetailsAsync(id);
                if (author == null)
                {
                    return Json(new { error = "Author not found." });
                }

                return Json(new { 
                    id = author.Id, 
                    name = author.Name, 
                    biography = author.Biography,
                    dateOfBirth = author.DateOfBirth.ToShortDateString(),
                    bookCount = author.Books.Count
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting author details: {ex.Message}");
                return Json(new { error = "An error occurred while fetching author details." });
            }
        }
    }
}