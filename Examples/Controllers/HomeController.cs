using Microsoft.AspNetCore.Mvc;
using ServiceList;
using System.Collections.Generic;
using GenericControllers;
using Entity;
using System;

namespace Examples.Controllers
{
    public class HomeController: GenericController<string>
    {
        readonly IDataService _data;
        public HomeController(IDataService data,
            ISelectDataService selectdata
           )
            : base(new List<Type>() {typeof(Data),typeof(SelectData)},new List<object>() { data, selectdata })
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
