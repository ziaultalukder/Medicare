using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicare.ViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}