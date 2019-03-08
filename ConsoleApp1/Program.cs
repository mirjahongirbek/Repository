using Newtonsoft.Json;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Data ast = new Data() { Name = "sdcsd" };
            var dfdf=JsonConvert.SerializeObject(ast);
        }
    }
    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
