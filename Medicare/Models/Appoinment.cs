using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicare.Models
{
    public class Appoinment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}