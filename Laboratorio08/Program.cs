using Laboratorio08.NeptunoDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio08
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] {0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.WriteLine("{0,1}",num);
            }
        }

        //lamba
        static void IntroLamba()
        {
            Func<int, bool> num = i => i % 2 == 0;
            int[] ints = new int[] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = from i in ints where num(i) select i;
            var numLamba = ints.Where(num);
        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        //Lamba
        static void DataSourceLamba()
        {
            var queryAllCustomers = context.clientes.ToList();
        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londes"
                                       select cust;
            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        //Lamba

        static void FilteringLamba()
        {
            var queryLondonCustomers = context.clientes.Where(
                cust => cust.Ciudad == "Londes").Select(cust => cust).ToList();
        }

        static void Ordering()
        {
            var queryLondonCustomers3 =
                from cust in context.clientes
                where cust.Ciudad == "London"
                orderby cust.NombreCompañia ascending
                select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        //LAMBA

        static void OrderingLamba()
        {
            var queryLondonCustomers3 = context.clientes.Where(
                cust => cust.Ciudad == "London").Select(cust => cust).ToList();
        }
        static void Grouping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach(cliente customer in customerGroup)
                {
                    Console.WriteLine("      {0}", customer.NombreCompañia);
                }
            }
        }

        //Lamba
        static void GroupingLamba()
        {
            var queryCustomerByCity = context.clientes.GroupBy(cust => (cust.Ciudad));
        }

        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        //Lamba
        static void Grouping2Lamba()
        {
            var custQuery = context.clientes.GroupBy(cust => (cust.Ciudad))
                .Where(custGroup => custGroup.Count() >2 )
                .OrderBy(custGroup => custGroup.Key).ToList();
        }

        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        //Lamba
        static void JoiningLamba()
        {
            var lambaJoinQuery = context.clientes.Join( dists,
                cust => cust,
                dist => dist.IdCliente, (cust, dist) => new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario });
        }
    }
}
