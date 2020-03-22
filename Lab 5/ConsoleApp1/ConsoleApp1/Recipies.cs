namespace ConsoleApp1
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class Recipies : DbContext
    {
        // Your context has been configured to use a 'Products' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConsoleApp1.Products' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Products' 
        // connection string in the application configuration file.
        public Recipies()
            : base("name=Products")
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Map(m =>
                {
                    m.Properties(p => new { p.SKU, p.Description, p.Price });
                    m.ToTable("Product", "BazaDeDate");
                })
                .Map(m =>
               {
                   m.Properties(p => new { p.SKU, p.ImageURL });
                   m.ToTable("ProductsWebInfo", "DazaDeDate");
               });
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }

    }

    public class RecipeServices
    {
        public void addElement(int sku, string description, decimal price, string imageURL)
        {
            using (var context = new Recipies())
            {
                var product = new Product
                {
                    SKU = sku,
                    Description = description,
                    ImageURL = imageURL,
                    Price = price
                };
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void printElements()
        {
            using (var context = new Recipies())
            {
                foreach (var p in context.Products)
                {
                    Console.WriteLine("{0} {1} {2} {3}", p.SKU, p.Description, p.Price.ToString("C"), p.ImageURL);
                }
            }

        }
    }
}