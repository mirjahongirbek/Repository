using Microsoft.AspNetCore.Mvc;
using ServiceList;
using System.Collections.Generic;

using Entity;
using System;
using System.Security.Claims;
using GenericControllers.Controllers;

namespace Examples.Controllers
{
    public class HomeController : GenericController<int>
    {
        public HomeController(ICompanyService company, IProductService product) : 
            base(new List<Type> { typeof(Company), typeof(Product)},
            new List<object> { company, product}
            )
        {

        }
    }

    //public class HomeController: GenericController<string>
    //{
    //   // readonly IDataService _data;
    //    public HomeController(IDataService data
        
    //       )
    //        : base(new List<Type>() {typeof(Data)},new List<object>() { data,  })
    //    {
            
    //    }
    //    public IActionResult Index()
    //    {
          
    //        return Ok();
    //    }

    //    public IActionResult Privacy()
    //    {
    //        var claims = new List<Claim>
    //            {
    //                new Claim(ClaimsIdentity.DefaultNameClaimType, ""),
    //                new Claim(ClaimsIdentity.DefaultRoleClaimType, "sd")
    //            };

    //        return Ok();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return Ok();
    //    }
    //}
    
}
