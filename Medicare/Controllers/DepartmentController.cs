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
    public class DepartmentController : Controller
    {
        // GET: Department
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var department = db.Departments.ToList();
            return View(department.Select(x => new DepartmentViewModel() {Id = x.Id, Name = x.Name, FilePath = x.FilePath}));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DepartmentViewModel departmentViewModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(departmentViewModel.Image.FileName);
            string extension = Path.GetExtension(departmentViewModel.Image.FileName);
            var fileNames = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
            string path = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
            fileName = Path.Combine(Server.MapPath("~/Images/DepartmentImages/"), fileNames);
            departmentViewModel.Image.SaveAs(fileName);

            Department department = new Department()
            {
                Name = departmentViewModel.Name,
                FilePath = path
            };
            db.Departments.Add(department);
            bool isSaved = db.SaveChanges() > 0;
            if (isSaved)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = db.Departments.Find(id);
            DepartmentViewModel departmentViewModel = new DepartmentViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                FilePath = department.FilePath
            };
            return View(departmentViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var department = db.Departments.Find(id);
            DepartmentViewModel departmentViewModel = new DepartmentViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                FilePath = department.FilePath
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentViewModel departmentViewModel)
        {
            if (departmentViewModel.Image != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(departmentViewModel.Image.FileName);
                string extension = Path.GetExtension(departmentViewModel.Image.FileName);
                var fileNames = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
                string path = fileName + DateTime.Now.ToString("yy-mm-dd") + extension;
                fileName = Path.Combine(Server.MapPath("~/Images/DepartmentImages/"), fileNames);
                departmentViewModel.Image.SaveAs(fileName);
                Department department = new Department()
                {
                    Id = departmentViewModel.Id,
                    Name = departmentViewModel.Name,
                    FilePath = path
                };
                db.Entry(department).State = EntityState.Modified;
                bool isSaved = db.SaveChanges() > 0;
                if (isSaved)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Department department = new Department()
                {
                    Id = departmentViewModel.Id,
                    Name = departmentViewModel.Name,
                    FilePath = departmentViewModel.FilePath
                };
                db.Entry(department).State = EntityState.Modified;
                bool isSaved = db.SaveChanges() > 0;
                if (isSaved)
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
            var department = db.Departments.Find(id);
            db.Entry(department).State = EntityState.Deleted;
            bool isDeleted = db.SaveChanges() > 0;
            if (isDeleted)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}