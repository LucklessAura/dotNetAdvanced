namespace ConsoleApp1
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class Photographs : DbContext
    {
        // Your context has been configured to use a 'Photographs' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConsoleApp1.Photographs' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Photographs' 
        // connection string in the application configuration file.
        public Photographs()
            : base("name=Photographs")
        {
        }
        public DbSet<Photograph> Photos { get; set; }

        public DbSet<PhotographFullImage> FullPhotos { get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Photograph>()
                .HasRequired(p => p.PhotographFullImage)
                .WithRequiredPrincipal(p => p.Photograph);
            modelBuilder.Entity<Photograph>()
                .ToTable("Photograph", "BazaDeDate");
            modelBuilder.Entity<PhotographFullImage>()
                .ToTable("Photograph", "BazaDeDate");
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
    public class Photograph
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public byte[] ThumbnailBits { get; set; }

        [ForeignKey("PhotoId")]
        public virtual PhotographFullImage PhotographFullImage { get; set; }
    }
    public class PhotographFullImage
    {
        [Key]
        public int PhotoID { get; set; }
        public byte[] HighResolutionBits { get; set; }
        [ForeignKey("PhotoId")]
        public virtual Photograph Photograph { get; set; }
    }


    public class PhotoServices
    {
        public void addElement(string title,byte[] thumbBits,byte[] fullBits)
        {
            using (var context = new Photographs())
            {
                var photo = new Photograph
                {
                    Title = title,
                    ThumbnailBits = thumbBits
                };
                var fullImage = new PhotographFullImage
                {
                    HighResolutionBits = fullBits,
                };
                photo.PhotographFullImage = fullImage;
                context.Photos.Add(photo);
                context.SaveChanges();
            }
        }

        public void printElements()
        {
            using (var context = new Photographs())
            {
                foreach (var p in context.Photos)
                {
                    Console.WriteLine("Photo : {0} , ThumbnailSize {1} bytes", p.Title, p.ThumbnailBits.Length);
                    context.Entry(p)
                        .Reference(ph => ph.PhotographFullImage).Load();
                    Console.WriteLine("Full Image Size: {0} bytes", p.PhotographFullImage.HighResolutionBits.Length);
                }
            }
        }
    }
}