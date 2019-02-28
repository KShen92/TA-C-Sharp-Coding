using CarInsuranceMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserInput(string firstName, string lastName, string emailAddress)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("InputError");
            }
            else
            {
                using (CarInsuranceMVCEntities1 db = new CarInsuranceMVCEntities1())
                {
                    var userdata = new UserData();
                    userdata.FirstName = firstName;
                    userdata.LastName = lastName;
                    userdata.EmailAddress = emailAddress;

                    db.UserDatas.Add(userdata);
                    db.SaveChanges();
                }
                return View("Success");
            }
        }


    }
}