using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using multimedia.Data;
using multimedia.Models;

namespace multimedia.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {exception.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var exist = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Books.Where(x => x.Id == book.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Title = book.Title;
                        exist.Author = book.Author;
                        exist.ISBN = book.ISBN;
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {exception.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Books.Where(x => x.Id == book.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        _context.Remove(exist);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {exception.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(book);
        }
    }
}
