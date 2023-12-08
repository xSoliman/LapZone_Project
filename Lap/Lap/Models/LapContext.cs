using Microsoft.EntityFrameworkCore;

namespace Lap.Models;

public class LapContext : DbContext
{
    public LapContext(DbContextOptions<LapContext> options)
    : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<VendorProduct> VendorProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderedProduct> OrderedProducts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }


    public LapContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your database connection string here
        optionsBuilder.UseSqlServer("Data Source=LAPTOP-1QPMNEL3;Initial Catalog=Zone2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure relationships using Fluent API
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Reviews)
            .WithOne(r => r.Customer) 
            .HasForeignKey(r => r.CustomerID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Carts)
            .WithOne(cart => cart.Customer)
            .HasForeignKey(cart => cart.CustomerID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.Customer)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CustomerID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VendorProduct>()
            .HasKey(vp => new { vp.VendorProductID });

        modelBuilder.Entity<VendorProduct>()
            .HasOne(vp => vp.Vendor)
            .WithMany(v => v.VendorProducts)
            .HasForeignKey(vp => vp.VendorID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VendorProduct>()
            .HasOne(vp => vp.Product)
            .WithMany(p => p.VendorProducts)
            .HasForeignKey(vp => vp.ProductID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderedProducts)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasKey(r => new { r.ReviewID });

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.OrderedProduct)
            .WithMany(op => op.Reviews)
            .HasForeignKey(r => r.OrderedProductID)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Cart>()
            .HasMany(cart => cart.CartProducts)
            .WithOne(cp => cp.Cart)
            .HasForeignKey(cp => cp.CartID)
             .OnDelete(DeleteBehavior.Restrict);
    }


}

