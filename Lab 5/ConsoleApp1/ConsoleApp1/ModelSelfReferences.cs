namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class ModelSelfReferences : DbContext
    {
        // Your context has been configured to use a 'ModelSelfReferences' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ConsoleApp1.ModelSelfReferences' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelSelfReferences' 
        // connection string in the application configuration file.
        public ModelSelfReferences()
            : base("name=ModelSelfReferences")
        {
        }

        public virtual DbSet<SelfReference> SelfReferences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SelfReference>()
                .HasMany(m => m.References)
                .WithOptional(m => m.ParentSelfReference);
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
    public class SelfReference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SelfReferenceId { get; private set; }
        public string Name { get; set; }
        public int? ParentSelfReferenceId { get; private set; }
        [ForeignKey("ParentSelfReferenceId")]
        public SelfReference ParentSelfReference { get; set; }
        public virtual ICollection<SelfReference> References { get; set; }
        public SelfReference() { References = new HashSet<SelfReference>(); }
    }



    public class SelfReferenceServices
    {
        public void addElement(string name, SelfReference parentSelfReference, List<SelfReference> references)
        {
            using (var context = new ModelSelfReferences())
            {
                SelfReference reference = new SelfReference();
                reference.Name = name;
                reference.ParentSelfReference = parentSelfReference;
                reference.References = references;
                context.SelfReferences.Add(reference);
                context.SaveChanges();
            }
        }


        public void printElements()
        {
            using (var context = new ModelSelfReferences())
            {
                var elemente = context.SelfReferences.ToList();
                elemente.ForEach(element => { Console.WriteLine(element.SelfReferenceId);  Console.WriteLine(element.Name); Console.WriteLine(element.ParentSelfReference); Console.WriteLine(element.References); }) ;
            }
        }
    }

}