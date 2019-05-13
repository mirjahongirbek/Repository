using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Marten;
using Marten.Schema;
using MartenRepository.Context;
using RepositoryRule.Base;
using RepositoryRule.Entity;

namespace MartenExample
{
    class Program
    {
        static void Main(string[] args)
        {
           var context=new Context();
            ProductService service = new ProductService(context);
            service.Add(new Product() {
                Name = "joha",
                SomeValue ="hello world",
                Child = new ChildClass() { Name="hello name", Name1="hello name2" },
                childClasses = new List<ChildClass>() { new ChildClass() {  Name="joha"} } });

            var product=  service.GetFirst(m => m.Name == "joha");
           var str= service.GetFirst(m => m.childClasses.Any(n => n.Name == "joha"));
            Console.ReadLine();

        }
    }
    public class Context : MartenRepository.Context.IMartenContext
    {
     public   Context()
        {
            Store =  Marten.DocumentStore.For(m=> {
                
                m.Connection("Host=172.17.9.248;Port=5432;Database=MarteTest;Username=postgres;Password=uzcard");
                m.HiloSequenceDefaults.MaxLo = 1;

            });
        }
        public IDocumentStore Store { get; set; }
    }
    public class Product:IEntity<int>
    {
        [Identity]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SomeValue { get; set; }
        public ChildClass Child { get; set; }
        
        public List<ChildClass> childClasses { get; set; }
    }
    public class ChildClass
    {
        public string Name { get; set; }
        public string Name1 { get; set; }
    }
    public class Company:IEntity<int>
    {
        public int Id { get; set; }
    }
    public class ProductService : MartenRepository.Repository.MartenRepository<Product, int>, IRepositoryBase<Product, int>
    {
        public ProductService(IMartenContext context) : base(context)
        {
        }
    }
   
}
