using Bridge.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bridge.Data
{
    public class BridgeDB:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public BridgeDB() { }
        public BridgeDB(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Category)
                .WithMany() 
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Request)
                .WithMany()
                .HasForeignKey(a => a.RequestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Category>().HasData(
                 new Category { Id = Guid.NewGuid(), Name = "עזרה טכנית", Icon = "🛠️" },
                 new Category { Id = Guid.NewGuid(), Name = "שינוע / קניות", Icon = "🚗" },
                 new Category { Id = Guid.NewGuid(), Name = "עזרה רפואית / ליווי", Icon = "❤️" },
                 new Category { Id = Guid.NewGuid(), Name = "תמיכה חברתית / שיחה", Icon = "💬" },
                 new Category { Id = Guid.NewGuid(), Name = "עזרה דתית / מניין", Icon = "📖" },
                 new Category { Id = Guid.NewGuid(), Name = "אירוח / סעודות שבת", Icon = "🏠" },
                 new Category { Id = Guid.NewGuid(), Name = "אוכל כשר ומוצרים", Icon = "🍎" },
                 new Category { Id = Guid.NewGuid(), Name = "שפה / בירוקרטיה", Icon = "📄" },
                 new Category { Id = Guid.NewGuid(), Name = "מידע והמלצות", Icon = "ℹ️" },
                 new Category { Id = Guid.NewGuid(), Name = "חירום / חילוץ", Icon = "🆘" }
             );
        }
    }
}
