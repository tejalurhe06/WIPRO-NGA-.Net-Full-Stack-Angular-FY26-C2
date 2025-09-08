
using LibraryManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Core.Models; 

namespace LibraryManagementSystem.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.GetGenresWithBooksAsync();
            return View(genres);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var genre = await _genreRepository.GetGenreWithDetailsAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepository.AddAsync(genre);
                await _genreRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _genreRepository.Update(genre);
                    await _genreRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GenreExists(genre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreRepository.GetGenreWithDetailsAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre != null)
            {
                _genreRepository.Remove(genre);
            }
            
            await _genreRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GenreExists(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return genre != null;
        }
    }
}