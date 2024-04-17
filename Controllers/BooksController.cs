using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BookStore.Data;
using BookStore.Models;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMongoCollection<Book> _books;

        public BooksController(IConfiguration configuration)
        {
            // Retrieve MongoDB connection string from appsettings.json
            string connectionString = configuration.GetConnectionString("BookStoreContext");

            // Create MongoClient and connect to the MongoDB database
            var client = new MongoClient(connectionString);

            // Get the database from the MongoClient
            var database = client.GetDatabase("DevOps"); // Replace "YourDatabaseName" with your actual database name

            // Get the collection from the database
            _books = database.GetCollection<Book>("Books");
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _books.Find(book => true).ToListAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,Genre,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _books.InsertOneAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Author,Genre,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _books.ReplaceOneAsync(b => b.Id == id, book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _books.Find(book => book.Id == id).FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
