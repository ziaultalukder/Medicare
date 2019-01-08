using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Medicare.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Medicare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        // GET: Role
        public ApplicationRoleManager _roleManager { get; set; }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult Index()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles.Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name }));
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (RoleManager.RoleExists(roleViewModel.Name))
            {
                TempData["msg"] = "Role Already Exist";
            }
            var identityRole = new IdentityRole(roleViewModel.Name);
            var result = RoleManager.Create(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var role = RoleManager.FindById(id);
            RoleViewModel roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel roleViewModel)
        {
            var updatedRole = new IdentityRole()
            {
                Id = roleViewModel.Id,
                Name = roleViewModel.Name
            };
            var result = RoleManager.Update(updatedRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            var role = RoleManager.FindById(id);
            var result = RoleManager.Delete(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //public PartialViewResult GetAll()
        //{
        //    var role = RoleManager.Roles.ToList();
        //    return PartialView("View", role.Select(x=> new RoleViewModel() {Id = x.Id, Name = x.Name}));
        //}

        //public PartialViewResult Create(RoleViewModel roleViewModel)
        //{
        //    var role = RoleManager.Roles.ToList();
        //    var identityRole = new IdentityRole(roleViewModel.Name);
        //    var result = RoleManager.Create(identityRole);
        //    if (result.Succeeded)
        //    {
        //        //return PartialView("Create", roleViewModel);
        //        return PartialView("View", role.Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name }));
        //    }
        //    return PartialView("Create", roleViewModel);
        //    //return PartialView("View", role.Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name }));
        //}
    }
}