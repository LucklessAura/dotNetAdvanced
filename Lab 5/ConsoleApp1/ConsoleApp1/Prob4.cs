namespace ConsoleApp1
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class Prob4 : DbContext
    {
        // Your context has been configured to use a 'Buisnesses' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConsoleApp1.Buisnesses' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Buisnesses' 
        // connection string in the application configuration file.
        public Prob4()
            : base("name=Buisnesses")
        {
        }

        DbSet<Buisness> Buisnesses { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    [Table("Buisness", Schema = "BazaDeDate")]
    public class Buisness
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuisnessId { get; protected set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
    }

    [Table("eCommerce", Schema = "BazaDeDate")]
    public class eCommerce : Buisness
    { public string URL { get; set; } }
    [Table("Retail", Schema = "BazaDeDate")]
    public class Retail : Buisness 
    { 
        public string Address { get; set; } 
        public string City { get; set; } 
        public string State { get; set; } 
        public string ZIPCode { get; set; } 
    }
}