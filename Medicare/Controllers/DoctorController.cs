using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Medicare.Models;
using Medicare.ViewModel;

namespace Medicare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        // GET: Doctor
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var department = db.Departments.ToList();
            var doctor = db.Doctors.ToList();
            return View(doctor.Select(x=> new DoctorViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FilePath = x.FilePath,
                Title = x.Title,
                Department = department.FirstOrDefault(c=>c.Id == x.DepartmentId)
            }));
        }

        public ActionResult Create()
        {
            var department = db.Departments.ToList();
            DoctorViewModel doctor = new DoctorViewModel();
            doctor.Departments = department;
            return View(doctor);
        }

        [HttpPost]
        public ActionResult Create(DoctorViewModel doctorViewModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(doctorViewModel.Image.FileName);
            string extension = Path.GetExtension(doctorViewModel.Image.FileName);
            var fileNames = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
            string path = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
            fileName = Path.Combine(Server.MapPath("~/Images/DoctorImages/"), fileNames);
            doctorViewModel.Image.SaveAs(fileName);
            Doctor doctor = new Doctor()
            {
                Name = doctorViewModel.Name,
                Title = doctorViewModel.Title,
                Description = doctorViewModel.Description,
                DepartmentId = doctorViewModel.DepartmentId,
                FilePath = path
            };
            db.Doctors.Add(doctor);
            bool isSaved = db.SaveChanges() > 0;
            if (isSaved)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            var department = db.Departments.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor = db.Doctors.Find(id);
            DoctorViewModel doctorViewModel = new DoctorViewModel()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Title = doctor.Title,
                Description = doctor.Description,
                FilePath = doctor.FilePath,
                Department = department.FirstOrDefault(c=>c.Id == doctor.DepartmentId)
            };
            return View(doctorViewModel);
        }

        public ActionResult Edit(int? id)
        {
            var department = db.Departments.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor = db.Doctors.Find(id);
            DoctorViewModel doctorViewModel = new DoctorViewModel()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Title = doctor.Title,
                Description = doctor.Description,
                FilePath = doctor.FilePath,
                Department = department.FirstOrDefault(c => c.Id == doctor.DepartmentId)
            };
            ViewBag.DepartmentId = new SelectList(department, "Id", "Name", doctor.DepartmentId);
            return View(doctorViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DoctorViewModel doctorViewModel)
        {
            if (doctorViewModel.Image != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(doctorViewModel.Image.FileName);
                string extension = Path.GetExtension(doctorViewModel.Image.FileName);
                var fileNames = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
                string path = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
                fileName = Path.Combine(Server.MapPath("~/Images/DoctorImages/"), fileNames);
                doctorViewModel.Image.SaveAs(fileName);
                Doctor doctor = new Doctor()
                {
                    Id = doctorViewModel.Id,
                    Name = doctorViewModel.Name,
                    Title = doctorViewModel.Title,
                    Description = doctorViewModel.Description,
                    DepartmentId = doctorViewModel.DepartmentId,
                    FilePath = path
                };
                db.Entry(doctor).State = EntityState.Modified;
                bool isUpdate = db.SaveChanges() > 0;
                if (isUpdate)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Doctor doctor = new Doctor()
                {
                    Id = doctorViewModel.Id,
                    Name = doctorViewModel.Name,
                    Title = doctorViewModel.Title,
                    Description = doctorViewModel.Description,
                    DepartmentId = doctorViewModel.DepartmentId,
                    FilePath = doctorViewModel.FilePath
                };
                db.Entry(doctor).State = EntityState.Modified;
                bool isUpdate = db.SaveChanges() > 0;
                if (isUpdate)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctor = db.Doctors.Find(id);
            db.Entry(doctor).State = EntityState.Deleted;
            bool isDeleted = db.SaveChanges() > 0;
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}