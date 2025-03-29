using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<BorrowedRequest> BorrowedRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            var users = User.PredefineUsers();
            modelBuilder.Entity<User>().HasData(users);

            modelBuilder.Entity<BorrowedRequest>()
                .HasOne(br => br.Book)
                .WithMany(b => b.BorrowedRequests)
                .HasForeignKey(br => br.BookId);

            modelBuilder.Entity<BorrowedRequest>()
                .HasOne(br => br.User)
                .WithMany(u => u.BorrowedRequests)
                .HasForeignKey(br => br.UserId);

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.BorrowedRequest)
                .WithOne()
                .HasForeignKey<BorrowedBook>(bb => bb.BorrowedRequestId);
        }
    }
}
