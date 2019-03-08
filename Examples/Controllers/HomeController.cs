using Microsoft.AspNetCore.Mvc;
using ServiceList;
using MongoDB.Bson;
using System.Collections.Generic;
using GenericControllers;
using Entity;
using System;

namespace Examples.Controllers
{
    public class HomeController: GenericsController<string>
    {
        readonly IDataService _data;
        public HomeController(IDataService data
           )
            : base(new List<Type>() {typeof(Data)},new List<object>() { data})
        {
        }
        public IActionResult Index()
        {
           // _data.Add(new Entity.Data() { Id = ObjectId.GenerateNewId().ToString(), Name = "sd" });
            return Ok();
        }

        public IActionResult Privacy()
        {
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Ok();
        }
    }
}
