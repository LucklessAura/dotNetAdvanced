using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            SelfReferenceServices service = new SelfReferenceServices();
            service.addElement("Element1", null, null);
            service.printElements();
            RecipeServices rService = new RecipeServices();
            rService.addElement(147, "Expandable Hydration Pack", 19.97M, "/pack147.jpg");
            rService.addElement(178, "Rugged Ranger Duffel Bag", 39.97M, "/pack178.jpg");
            rService.addElement(186, "Range Field Pack", 98.97M, "/noimage.jp");
            rService.addElement(202, "Small Deployment Back Pack", 29.97M, "/pack202.jpg");
            rService.printElements();
            Console.ReadKey();
        }
    }
}
