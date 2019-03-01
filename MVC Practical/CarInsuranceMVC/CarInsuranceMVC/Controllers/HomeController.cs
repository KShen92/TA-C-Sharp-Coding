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
        public ActionResult UserInput(string firstName, string lastName, string emailAddress, short carYear, string carMake, 
            string carModel, bool DUI, short speedingTickets, bool fullCoverage, DateTime dateOfBirth = new DateTime())
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
                    userdata.DateOfBirth = dateOfBirth;
                    userdata.CarYear = carYear;
                    userdata.CarMake = carMake;
                    userdata.CarModel = carModel;
                    if (DUI)
                    {
                        userdata.DUI = true;
                    }
                    else
                    {
                        userdata.DUI = false;
                    }
                    userdata.SpeedingTickets = speedingTickets;
                    if (fullCoverage)
                    {
                        userdata.FullCoverage = true;
                    }
                    else
                    {
                        userdata.FullCoverage = false;
                    }

                    db.UserDatas.Add(userdata);
                    db.SaveChanges();
                }
                return View("Quote");
            }
        }


    }
}