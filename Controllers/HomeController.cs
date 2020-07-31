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
        public ActionResult ViewCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);

            }
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return View(customer);
            }
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();

            return RedirectToAction("customerList");
        }

        public ActionResult EditCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);

            }
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer, string Id)
        {
            Customer customerToEdit = customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customerToEdit.Name = customer.Name;
                customerToEdit.Telephone = customer.Telephone;
                SaveCache();

                return RedirectToAction("CustomerList");
            }
        }

        // GET
        public ActionResult DeleteCustomer(string id)
        {
            Customer customer = customers.FirstOrDefault(c => c.Id == id);
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);

            }
        }

        [HttpPost, ActionName("DeleteCustomer")]
        public ActionResult DeleteCustomer(Customer customer, string id)
        {
            Customer customerToDelete = customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customers.Remove(customerToDelete);
                SaveCache();

                return RedirectToAction("CustomerList");
            }
        }


        public ActionResult CustomerList()
        {


            return View(customers);
        }
    }
}