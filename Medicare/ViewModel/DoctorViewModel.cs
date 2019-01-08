using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medicare.Models;

namespace Medicare.ViewModel
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Department> Departments { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}