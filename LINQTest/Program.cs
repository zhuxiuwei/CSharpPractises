using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var customers = new[] {
                new { CustomerID = 1, FirstName = "Kim", LastName = "Abercrombie",
                CompanyName = "Alpine Ski House" },
                new { CustomerID = 2, FirstName = "Jeff", LastName = "Hay",
                CompanyName = "Coho Winery" },
                new { CustomerID = 3, FirstName = "Charlie", LastName = "Herb",
                CompanyName = "Alpine Ski House" },
                new { CustomerID = 4, FirstName = "Chris", LastName = "Preston",
                CompanyName = "Trey Research" },
                new { CustomerID = 5, FirstName = "Dave", LastName = "Barnett",
                CompanyName = "Wingtip Toys" },
                new { CustomerID = 6, FirstName = "Ann", LastName = "Beebe",
                CompanyName = "Coho Winery" },
                new { CustomerID = 7, FirstName = "John", LastName = "Kane",
                CompanyName = "Wingtip Toys" },
                new { CustomerID = 8, FirstName = "David", LastName = "Simpson",
                CompanyName = "Trey Research" },
                new { CustomerID = 9, FirstName = "Greg", LastName = "Chapman",
                CompanyName = "Wingtip Toys" },
                new { CustomerID = 10, FirstName = "Tim", LastName = "Litton",
                CompanyName = "Wide World Importers" }
            };

            var addresses = new[] {
                new { CompanyName = "Alpine Ski House", City = "Berne",
                Country = "Switzerland"},
                new { CompanyName = "Aoho Winery", City = "San Francisco",
                Country = "United States"},
                new { CompanyName = "Trey Research", City = "New York",
                Country = "United States"},
                new { CompanyName = "Wingtip Toys", City = "London",
                Country = "United Kingdom"},
                new { CompanyName = "Wide World Importers", City = "Tetbury",
                Country = "United Kingdom"}
             };

            //Select first names
            Console.WriteLine("==Select first names==");
            IEnumerable<string> customerFirstNamrs = customers.Select(cust => cust.FirstName);
            foreach(string firstName in customerFirstNamrs){
                Console.Write(firstName + " ");
            }
            Console.WriteLine("\r\n");


            //Select first name and last name
            Console.WriteLine("==Select first name and last name==");
            //方式1，拼接字符串
            IEnumerable<string> customerFirstAndLastNamrs = customers.Select(cust => cust.FirstName + " | " + cust.LastName );
            foreach(string combinedName in customerFirstAndLastNamrs)
                Console.WriteLine(combinedName);
            //方式2，定义新的对象（匿名类）
            var customerFirstAndLastNamrs2 = customers.Select(cust => new { FirstName = cust.FirstName, LastName = cust.LastName });
            foreach (var names in customerFirstAndLastNamrs2)
                Console.WriteLine("FirstName=" + names.FirstName + ", LastName=" + names.LastName);
            Console.WriteLine("\r\n");

            //Select company name in US
            Console.WriteLine("==Select company name in US==");
            IEnumerable<String> usCompanies = addresses
                .Where(addr => addr.Country.Equals("United States"))
                .Select(addr => addr.CompanyName);
            foreach (string company in usCompanies)
                Console.WriteLine(company);
            Console.WriteLine("\r\n");

            //group by
            Console.WriteLine("==Group By==");
            var companiesGroupedByCountry = addresses.GroupBy(addrs => addrs.Country);
            foreach (var companiesPerCountry in companiesGroupedByCountry)
            {
                Console.WriteLine("Country: {0}\t{1} companies",
                companiesPerCountry.Key, companiesPerCountry.Count());
                foreach (var companies in companiesPerCountry)
                    Console.WriteLine("\t{0}", companies.CompanyName);
            }
            Console.WriteLine("\r\n");

            //join. Display firstnamem last name(in customers array) and country(in address array)
            Console.WriteLine("==Join==");
            var customerNameAndCountry = customers
                .Select(c => new { c.FirstName, c.LastName, c.CompanyName })
                .Join(addresses, cus => cus.CompanyName, add => add.CompanyName, 
                (cus, add) => new { cus.FirstName, cus.LastName, add.Country });
            foreach (var item in customerNameAndCountry)
                Console.WriteLine(item.FirstName + ", " + item.LastName + ", " + item.Country);
            Console.WriteLine("\r\n");


            /****************** Using SQL like statements ******************/
            Console.WriteLine("==SQL statements: where orderby ==");
            var addrsInUsOrdered = from addr in addresses
                                   where addr.Country.Equals("United States")
                                   orderby addr.CompanyName descending
                                   select addr.CompanyName;
            foreach (var item in addrsInUsOrdered)
                Console.WriteLine(item);
            Console.WriteLine("\r\n");

            //group by
            Console.WriteLine("==SQL statements: where orderby ==");
            var companiesGroupedByCountry2 = from a in addresses
                                             group a by a.Country;
            foreach (var companiesPerCountry in companiesGroupedByCountry2)
            {
                Console.WriteLine("Country: {0}\t{1} companies",
                companiesPerCountry.Key, companiesPerCountry.Count());
                foreach (var companies in companiesPerCountry)
                    Console.WriteLine("\t{0}", companies.CompanyName);
            }
            Console.WriteLine("\r\n");

            //count
            Console.WriteLine("==SQL statements: count ==");
            int conutryNum = (from addr in addresses
                              select addr.Country).Distinct().Count();
            Console.WriteLine("Country number: " + conutryNum + "\r\n");

            //join
            Console.WriteLine("==SQL statements: join ==");
            var customerNameAndCountry2 = from c in customers
                                          join a in addresses
                                          on c.CompanyName equals a.CompanyName
                                          orderby c.FirstName
                                          select new { c.FirstName, c.LastName, a.Country };
            foreach (var item in customerNameAndCountry2)
                Console.WriteLine(item.FirstName + ", " + item.LastName + ", " + item.Country);
            Console.WriteLine("\r\n");

            Console.ReadLine();
        }
    }
}
