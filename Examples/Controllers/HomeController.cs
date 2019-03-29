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
        public HomeController(ICompanyService company, IProductService product, ICategoryService category) : 
            base(new List<Type> { typeof(Company), typeof(Product), typeof(Category)},
            new List<object> { company, product, category}
            )
        {

        }
    }

    
}
