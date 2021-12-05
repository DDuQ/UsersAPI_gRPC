
using Microsoft.EntityFrameworkCore;

namespace grpcserver.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<UserModel>().HasKey(u => u.DocumentId);
        }
    }
}
