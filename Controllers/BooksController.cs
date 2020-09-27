using Microsoft.AspNetCore.Mvc;
using MongoTutorialDemo.Models;
using MongoTutorialDemo.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("genre/{genre}")]
        public IActionResult GetByCategory(string genre)
        {
            var books = _bookService.GetByCategory(genre);

            return Ok(books);
        }

        [HttpGet("{key}")]
        public IActionResult GetByNameKey(string key)
        {
            var books = _bookService.FindByNamekey(key);

            return Ok(books);
        }

        [HttpGet("BulkInsert")]
        public async Task<IActionResult> BulkInsert()
        {
            return Ok(await _bookService.BulkInsert());
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpPut("BulkUpdate")]
        public IActionResult BulkUpdate(string oldName, string newName)
        {
            return Ok(_bookService.BulkUpdate(oldName, newName));
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }

        [HttpDelete("BulkDelete")]
        public IActionResult DeleteAll()
        {
            return Ok(_bookService.BulkDelete());
        }
    }
}