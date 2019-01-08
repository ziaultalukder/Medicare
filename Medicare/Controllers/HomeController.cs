using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medicare.Models;
using Medicare.ViewModel;

namespace Medicare.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            
            var doctor = db.Doctors.ToList();
            AppoinmentViewModel appoinment = new AppoinmentViewModel();

            appoinment.Doctors = doctor;

            return View(appoinment);
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Department()
        {
            var department = db.Departments.ToList();
            return View(department);
        }

        public ActionResult Doctor()
        {
            var doctor = db.Doctors.ToList();
            return View(doctor);
        }

        public ActionResult Appointment()
        {
            var doctor = db.Doctors.ToList();
            AppoinmentViewModel appoinment = new AppoinmentViewModel();
            appoinment.Doctors = doctor;
            return PartialView("Appointment", appoinment);
        }
        [HttpPost]
        public ActionResult Appointment(AppoinmentViewModel appoinmentViewModel)
        {
            if (ModelState.IsValid)
            {
                Appoinment appoinment = new Appoinment()
                {
                    Id = appoinmentViewModel.Id,
                    Name = appoinmentViewModel.Name,
                    Contact = appoinmentViewModel.Contact,
                    Date = appoinmentViewModel.Date,
                    DoctorId = appoinmentViewModel.DoctorId
                };
                db.Appoinments.Add(appoinment);
                bool isSaved = db.SaveChanges() > 0;
                if (isSaved)
                {
                    TempData["msg"] = "Your Appoinment Save Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}