using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Models;
using System;

namespace SchuseOnlineShop.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandLogo> BrandLogos { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySubCategory> CategorySubCategories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductVideo> ProductVideos { get; set; }
        public DbSet<SectionHeader> SectionHeaders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<HomeCategory> HomeCategories { get; set; }
        public DbSet<Advert> Adverts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<SubCategory>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<SubCategory>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<ProductSize>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<CategorySubCategory>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Brand>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Size>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Color>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<ProductVideo>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Team>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Setting>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<SectionHeader>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Advert>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<ProductComment>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Contact>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.SoftDelete);
            modelBuilder.Entity<CartProduct>().HasQueryFilter(p => !p.SoftDelete);

        }
    }
}
