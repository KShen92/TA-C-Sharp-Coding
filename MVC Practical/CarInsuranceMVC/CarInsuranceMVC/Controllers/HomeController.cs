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
                if (DUI){userdata.DUI = true;}
                else{userdata.DUI = false;}
                userdata.SpeedingTickets = speedingTickets;
                if (fullCoverage){userdata.FullCoverage = true;}
                else{userdata.FullCoverage = false;}

                DateTime current = DateTime.Now;
                int age = current.Year - dateOfBirth.Year;
                if (dateOfBirth > current.AddYears(-age)) age--;

                userdata.Quote = 50;

                if (age < 18) { userdata.Quote = userdata.Quote + 100; }
                else if (age < 25 || age > 100) { userdata.Quote = userdata.Quote + 25; }

                if (userdata.CarYear < 2000 || userdata.CarYear > 2015) { userdata.Quote = userdata.Quote + 25; }

                string makeString = carMake.ToLower();
                string modelString = carModel.ToLower();
                if (makeString == "porsche")
                {
                    userdata.Quote = userdata.Quote + 25;
                    if (modelString == "911 carrera")
                    {
                        userdata.Quote = userdata.Quote + 25;
                    }
                }

                int ticketTotal = speedingTickets * 10;
                userdata.Quote = userdata.Quote + ticketTotal;
                if (DUI) { userdata.Quote = (userdata.Quote * 125 / 100); }
                if (fullCoverage) { userdata.Quote = (userdata.Quote * 150 / 100); }

                float roundQuote = (float)(Math.Round((double)userdata.Quote, 2));
                ViewBag.Message = "Your monthly quote is $" + roundQuote;

                db.UserDatas.Add(userdata);
                db.SaveChanges();            
            }
            return View("Quote");
        }


    }
}