using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DBContext : DbContext
    {
        public DBContext()
        {

        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task1> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }




    }
}
