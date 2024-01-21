using FullStackApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Data
{
    public class OnlineLibraryDbContext: DbContext
    {
        public OnlineLibraryDbContext(DbContextOptions options) : base(options) { 
        }

        public DbSet<Book> Books { get; set; }
    }
}
