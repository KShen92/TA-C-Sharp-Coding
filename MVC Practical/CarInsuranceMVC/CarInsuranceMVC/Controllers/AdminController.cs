using CarInsuranceMVC.Models;
using CarInsuranceMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (CarInsuranceMVCEntities1 db = new CarInsuranceMVCEntities1())
            {
                var userdatas = db.UserDatas.ToList();
                var adminVms = new List<AdminVm>();
                foreach (var userdata in userdatas)
                {
                    var adminVm = new AdminVm();
                    adminVm.FirstName = userdata.FirstName;
                    adminVm.LastName = userdata.LastName;
                    adminVm.EmailAddress = userdata.EmailAddress;
                    adminVm.Quote = "$" + userdata.Quote.ToString();
                    adminVms.Add(adminVm);
                }

                return View(adminVms);
            }
        }
    }
}