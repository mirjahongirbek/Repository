using ServiceList;
using System.Collections.Generic;
using Entity;
using System;
using GenericControllers.Controllers;
using LangServer.Services.Interfaces;
using System.Threading.Tasks;
using LanguageService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Entity;
using SiteResponse;

namespace Examples.Controllers
{

    public class HomeController : ControllerBase// GenericController<int>
    {
        public HomeController()
        {

        }
        //ILanguageService<int> _language;
        //public HomeController(ICompanyService company, IProductService product, ICategoryService category,
        //    ILanguageService<int> language) : 
        //    base(new List<Type> { typeof(Company), typeof(Product), typeof(Category)},
        //    new List<object> { company, product, category}
        //    )
        //{
        //    _language = language;
        //}
        public async Task<ResponseData> Index2()
        {
            ResponseData response = new ResponseData(Responses.ServiceNotFound)
            {
                error = "Invalid user information",
                statusCode = 444
            };
            Console.WriteLine(response.Responses);
         return   this.GetResponse(response);
        }
        [HttpGet]
        public async Task<string> Index1()
        {
            return null;
         //   await _language.GetFirstBy<Product>(1, m=>m.CompanyId==2);
         //var sd= await _language.GetList<Product>(1);
         //   return "hello world";
        }
    }

    
}
