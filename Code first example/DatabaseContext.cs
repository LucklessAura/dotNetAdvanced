using System;
using Microsoft.EntityFrameworkCore;
using Files;
using System.Collections.Generic;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<File> Files {set; get;}
        public DbSet<AdditionalFileProprieties> AdditionalFileProprieties {set;get;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                // trebuie modificat DESKTOP-V4Q9TES cu serverul SQL de pe calculatorul pe care se testeaza functia
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-V4Q9TES;Database=Alpha;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //declara campurile cu restrictii si cheile primare din tabele


            //keya primara a tabelei Files
            modelBuilder.Entity<File>()
             .HasKey(t => t.FileID);

            modelBuilder.Entity<File>()
             .Property(t => t.FilePath)
             .IsRequired()
             .HasMaxLength(100);

            modelBuilder.Entity<File>()
             .Property(t => t.DateCreated)
             .IsRequired();

            modelBuilder.Entity<File>()
             .Property(t => t.Occasion)
             .HasMaxLength(100);

            modelBuilder.Entity<File>()
             .Property(t => t.Place)
             .HasMaxLength(100);
        
            modelBuilder.Entity<AdditionalFileProprieties>()
             .Property(t => t.FileFK)
             .IsRequired();

            modelBuilder.Entity<AdditionalFileProprieties>()
             .Property(t => t.ProprietyName)
             .IsRequired()
             .HasMaxLength(50);

            modelBuilder.Entity<AdditionalFileProprieties>()
             .Property(t => t.ProprietyValue)
             .IsRequired()
             .HasMaxLength(100);

            //Foreign key-ul tabelei AdditionalFileProprieties catre Files
            modelBuilder.Entity<AdditionalFileProprieties>()
             .HasOne(t => t.File)
             .WithMany(t => t.Proprieties)
             .HasForeignKey( t => t.FileFK);
            
            //Primary key-ul tabelei AdditionalFileProprieties
            modelBuilder.Entity<AdditionalFileProprieties>()
             .HasKey(t => t.ProprietyID);
        

        }
    }

    public class Services
    {
        public void addTestValue()
        {
            // este doar o functie pentru a teta validitatea tabelelor
            using (var context = new DatabaseContext())
            {
                File file1 = File.Create("path to the file",DateTime.Now,"occasion","person1, person2", "place",null);
                
                AdditionalFileProprieties addProp = AdditionalFileProprieties.Create(file1.FileID,"new prop","prop val",file1);
                List<AdditionalFileProprieties> listOfProprieties = new List<AdditionalFileProprieties>();
                listOfProprieties.Add(addProp);
                file1.Proprieties = listOfProprieties;
                context.Files.Add(file1);
                context.AdditionalFileProprieties.Add(addProp);
                context.SaveChanges();
            }
        }


        public Boolean addNewPropriety(File file,String proprietyName,String proprietyValue)
        {
            //adauga o noua coloana in AdditionalFileProprieties pentru fisierul dat
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }

        public Boolean addNewFile(String path,DateTime creationDate,String occasion, String people, String place,List<(String,String)> additionalProprieties)
        {
            //adauga un nou fisier in sistem la path-ul dat( in caz ca se lucreaza cu directoare si subdirectoare)
            //implica is adaugarea de proprietati noi pentru poza daca lista aceea are elemente
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }

        public Boolean bulkAddFiles(List<File> filesToAdd)
        {
            //pentru fiecare fisier din lista creeaza un prompt pentru adaugarea de proprieta si cheama addNewFile pentru fisier si proprietati
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }

        public Boolean modifyFile(File file,String path,DateTime creationDate,String occasion, String people, String place,List<(String,String)> additionalProprieties)
        {
            //modifica un fisier existent, daca fisierul nu exista poate creea exceptii
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }

        public Boolean deleteFile(File file)
        {
            //marcheaza un fisier pentru stergere / muta fisierul in "cosul de gunoi"
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }

        public Boolean definitiveDeleteFile(File file)
        {
            //sterge definitiv un fisier care este deja in "cosul de gunoi"
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }

        public List<File> findFilesByAdditionalProprieties(List<(String,String)> additionalProprieties)
        {
            // returneaza o lista cu toate fisierele care au toate proprietatile din lista
            return new List<File>();
        }

        public List<File> findFilesByDate(DateTime startDate, DateTime endDate)
        {
            // returneaza o lista cu toate fisierele care au data creeari intre startDate si endDate
            return new List<File>();
        }

        public List<File> findFilesByFilePath(String path)
        {
            // returneaza o lista cu toate fisierele care au path-ul path sau care sunt in directorul/ subdirectorul dat de path
            return new List<File>();
        }

        public List<File> findFilesByFileOcasion(String path)
        {
            // returneaza o lista cu toate fisierele care au campul Path path sau care sunt in directorul/ subdirectorul dat de path
            return new List<File>();
        }

        public List<File> findFilesByFilePeople(String people)
        {
            // returneaza o lista cu toate fisierele care au in campul People toate numele din people(valorile sunt ca string comma separated)
            return new List<File>();
        }

        public List<File> findFilesByFilePlace(String place)
        {
            // returneaza o lista cu toate fisierele care au campul Place egal cu place
            return new List<File>();
        }


        public Boolean saveFilesToPath(List<File> filesToSave,String path)
        {
            //copiaza fisierele din filesToSave in path
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }


        public Boolean moveFilesToPath(List<File> filesToSave,String path)
        {
            //muta fisierele din filesToSave in path, path nu poate sa fie un suport extern
            //returneaza true daca operatia a fost realizata cu succes, false altfel
            return true;
        }


    }
}