using LangEntity.Language;
using LangServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryRule.Entity;
using SiteResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LangServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LanguageController : Controller
    {
        ILanguageService _lang;
        public LanguageController(ILanguageService lang)
        {
            _lang = lang;
        }
        [HttpGet]
        public async Task<ResponseData> GetAll()
        {
            try
            {

                return this.GetResponse(_lang.FindAll());
            }
            catch (Exception ext)
            {
                return this.GetResponse(ext);

            }

        }
        public async Task<ResponseData> Add([FromBody]Language model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.GetResponse(model);
                }
                var lang = _lang.GetFirst(m => m.LanguageId == model.LanguageId);
                if (lang != null)
                {
                    return this.GetResponse(lang);
                }
                await _lang.AddAsync(model);
                State.State.Languages.Add(model);
                return this.GetResponse( model);
            }
            catch (Exception ext)
            {
                return this.GetResponse(ext);
            }
        }

    }
}