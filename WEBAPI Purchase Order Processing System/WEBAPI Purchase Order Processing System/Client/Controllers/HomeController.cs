using Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient httpClient = new HttpClient();
        string apiBaseUrl = "http://localhost/popsapi/api/";
        // GET: Home
        public ActionResult Index()
        {
            var appViewModel = new AppViewModel();

            var response = httpClient.GetAsync(apiBaseUrl + "supplier/get");
            var stringResponse = response.Result.Content.ReadAsStringAsync().Result;
            appViewModel.Suppliers = JsonConvert.DeserializeObject<List<SupplierModel>>(stringResponse);
            return View(appViewModel);
        }
    }
}