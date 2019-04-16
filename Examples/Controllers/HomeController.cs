using ServiceList;
using System.Collections.Generic;
using Entity;
using System;
using GenericControllers.Controllers;
using LangServer.Services.Interfaces;
using System.Threading.Tasks;
using LanguageService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Examples.Controllers
{

    public class HomeController : GenericController<int>
    {
        ILanguageService<int> _language;
        public HomeController(ICompanyService company, IProductService product, ICategoryService category,
            ILanguageService<int> language) : 
            base(new List<Type> { typeof(Company), typeof(Product), typeof(Category)},
            new List<object> { company, product, category}
            )
        {
            _language = language;
        }
        [HttpGet]
        public async Task<string> Index1()
        {
            await _language.Search<Product>(2,m=>m.CategoryId,1);
          var sd= await _language.GetList<Product>(1);
            return "hello world";
        }
    }

    
}
