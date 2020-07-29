using codeAlongCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Runtime.Caching;
using System.Security.Cryptography.X509Certificates;

namespace codeAlongCS.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cashe = MemoryCache.Default;
        List<Customer> customers;
        
        public HomeController()
        {
            customers = cashe["customers"] as List<Customer>;
            if(customers == null)
            {
                customers = new List<Customer>();
            }
        }
        public void SaveCache()
        {
            cashe["customers"] = customers;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MySuperP = "THis is something something";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ViewCustomer(Customer postedCustomer)
        {
            Customer customer = new Customer();
            customer.Id = Guid.NewGuid().ToString();
            customer.Name = postedCustomer.Name;
            customer.Telephone = postedCustomer.Telephone;

            return View(customer);

        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();

            return RedirectToAction("customerList");
        }

        public ActionResult CustomerList()
        {


            return View(customers);
        }
    }
}