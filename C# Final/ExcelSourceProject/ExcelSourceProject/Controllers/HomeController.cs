using ExcelSourceProject.Models;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExcelSourceProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()        
        {
            string filePath = "ExcelSource.xlsx";
            var excelSource = new ExcelQueryFactory(filePath);
            var readSource = from x in excelSource.Worksheet() select x;

            using (ExcelSourceEntities db = new ExcelSourceEntities())
            {
                foreach (var x in readSource)
                {
                    string firstName = x["FirstName"];
                    string lastName = x["LastName"];
                    string emailAddress = x["EmailAddress"];

                    var excelsource = new ExcelSource();
                    excelsource.FirstName = firstName;
                    excelsource.LastName = lastName;
                    excelsource.EmailAddress = emailAddress;

                    db.ExcelSources.Add(excelsource);
                    db.SaveChanges();
                }
            }
            return View();
        }
    }
}