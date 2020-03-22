using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModelDesignFirst_L1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Model Design First");
            //TestPerson();
            TestOneToMany();
            Console.ReadKey();
        }


        static void TestOneToMany()
        {
            Console.WriteLine("One To Many Association");
            using (Model1Container context = new Model1Container())
            {
                string name, city;
                string newOrder;
                int totalValue;
                Console.WriteLine("Name:");
                name = Console.ReadLine();

                Console.WriteLine("City:");
                city = Console.ReadLine();
                Customer c = new Customer() { Name = name, City = city };
                Console.WriteLine("New Order (y/n):");
                newOrder = Console.ReadLine();
                context.Customers.Add(c);
                while (newOrder == "t")
                 {
                    Console.WriteLine("Value:");
                    totalValue =  int.Parse(Console.ReadLine());
                    Console.WriteLine("New Order (y/n):");
                    newOrder = Console.ReadLine();
                    Order o1 = new Order() { TotalValue = totalValue, Date = DateTime.Now, Customer = c };
                    context.Orders.Add(o1);
                }
                context.SaveChanges();

                var items = context.Customers;
                foreach(var x in items)
                {
                    Console.WriteLine("Customer: {0}, {1}, {2}", x.CustomerId, x.Name, x.City);
                    foreach(var y in x.Orders)
                    {
                        Console.WriteLine("\tOrder: {0}, {1}, {2}", y.OrderId, y.TotalValue, y.Date);
                    }
                }
            }
        }

        static void TestPerson()
        {
            using (Model1Container context = new Model1Container())
            {
                string firstName, lastName, middleName, telephoneNumber;
                Console.WriteLine("Fist Name:\n");
                firstName = Console.ReadLine();
                Console.WriteLine("Last Name:\n");
                lastName = Console.ReadLine();
                Console.WriteLine("Middle Name:\n");
                middleName = Console.ReadLine();
                Console.WriteLine("Telephone Nuber:\n");
                telephoneNumber = Console.ReadLine();
                Person p = new Person()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    TelephoneNumber = telephoneNumber
                };
                context.People.Add(p);
                context.SaveChanges();
                var items = context.People;
                foreach (var x in items)
                    Console.WriteLine("{0} {1}", x.Id, x.FirstName);
            }
        }
    }
}
