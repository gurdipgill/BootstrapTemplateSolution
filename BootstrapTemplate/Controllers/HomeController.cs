using BootstrapTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace BootstrapTemplate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var obj = new MultipleSelectSearchViewModel { SearchModelList = new List<SearchModel>() };
            obj.SearchModelList = CountryDate();
            return View(obj);
        }

        private List<SearchModel> CountryDate()
        {
            var model = new List<SearchModel>
            {
                new SearchModel {Name = "America"},
                new SearchModel {Name = "India"},
                new SearchModel {Name = "Sri Lanka"},
                new SearchModel {Name = "China"},
                new SearchModel {Name = "Pakistan"}
            };
            return model;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
