using ExcelSourceProject.Models;
using ExcelSourceProject.View_Models;
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
            return View();
        }

        public ActionResult ReadData()        
        {
            string filePath = @"D:\Tech Academy\TA-C-Sharp-Coding\C# Final\ExcelSourceProject\ExcelSource.xlsx";
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
            return View("ReadSuccess");
        }

        public ActionResult DisplayData()
        {
            using (ExcelSourceEntities db = new ExcelSourceEntities())
            {
                var userdata = db.ExcelSources.ToList();
                var dataVms = new List<DataVm>();
                foreach (var user in userdata)
                {
                    var dataVm = new DataVm();
                    dataVm.FirstName = user.FirstName;
                    dataVm.LastName = user.LastName;
                    dataVm.EmailAddress = user.EmailAddress;
                    dataVms.Add(dataVm);
                }
                return View(dataVms);
            }
        }
    }
}