using System;
using System.Collections.Generic;

namespace Files
{
    public class File
    {
        private File()
        {

        }
        public Guid FileID {private set; get;}
        
        public String FilePath {private set; get;}

        public DateTime DateCreated {private set; get;}
         public String Occasion {private set; get;}

        public String People {private set; get;}

        public String Place {private set; get;}

        public List<AdditionalFileProprieties> Proprieties {set;get;}

        public static File Create(String filePath,DateTime dateCreated, String occasion, String people,String place,List<AdditionalFileProprieties> proprieties)
        {
            return new File{
                FileID = Guid.NewGuid(),
                FilePath = filePath,
                DateCreated = dateCreated,
                Occasion = occasion,
                People = people,
                Place = place,
                Proprieties = proprieties
            };
        }
    }

    public class AdditionalFileProprieties
    {
        private AdditionalFileProprieties()
        {

        }
        public Guid ProprietyID {private set; get;}

        public Guid FileFK {private set; get;}

        public String ProprietyName {private set; get;}

        public String ProprietyValue {private set;get;}

        public virtual File File {private set; get;}

        public static AdditionalFileProprieties Create(Guid fileFK, String proprietyName,String proprietyValue,File file)
        {
            return new AdditionalFileProprieties{
                ProprietyID = Guid.NewGuid(),
                FileFK = fileFK,
                ProprietyName = proprietyName,
                ProprietyValue = proprietyValue,
                File = file
            };
        }
    }
}