using FullStackApi.Data;
using FullStackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly OnlineLibraryDbContext _onlineLibraryContext;
        public BooksController(OnlineLibraryDbContext onlineLibraryContext) {
            _onlineLibraryContext = onlineLibraryContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _onlineLibraryContext.Books.ToListAsync();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book bookRequest)
        {
            bookRequest.Id = Guid.NewGuid();

            await _onlineLibraryContext.Books.AddAsync(bookRequest);
            await _onlineLibraryContext.SaveChangesAsync();

            return Ok(bookRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBook([FromRoute] Guid id)
        {

            var book = await _onlineLibraryContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null) {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, Book updateBookRequest)
        {

            var book = await _onlineLibraryContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = updateBookRequest.Title;
            book.Author = updateBookRequest.Author;
            book.Type = updateBookRequest.Type;
            book.Description = updateBookRequest.Description;
            book.SerialNumber = updateBookRequest.SerialNumber;
            book.Location = updateBookRequest.Location;

            await _onlineLibraryContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {

            var book = await _onlineLibraryContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            _onlineLibraryContext.Books.Remove(book);
            await _onlineLibraryContext.SaveChangesAsync();
            return Ok(book);
        }


    }
}
