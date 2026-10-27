using Microsoft.EntityFrameworkCore;
using ComicStore.API.Models;

namespace ComicStore.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Comic> Comics { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Du lieu mau
        modelBuilder.Entity<Comic>().HasData(
            new Comic { Id = 1, Title = "One Piece tap 1", Author = "Oda Eiichiro", Genre = "Hanh dong", Price = 35000, Stock = 50, Description = "Cuoc hanh trinh cua Luffy" },
            new Comic { Id = 2, Title = "Naruto tap 1", Author = "Masashi Kishimoto", Genre = "Hanh dong", Price = 30000, Stock = 30, Description = "Nhat ky ninja" },
            new Comic { Id = 3, Title = "Doraemon tap 1", Author = "Fujiko Fujio", Genre = "Hai huoc", Price = 25000, Stock = 100, Description = "Meo may den tu tuong lai" }
        );
    }
}
