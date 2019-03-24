using Entity;
using ServiceList;
using System;

namespace SQlLiteExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DataBase database = new DataBase();
            CompanyService company = new CompanyService(database);
           var list= company.FindReverse(0, 10);
        }
    }
}
