using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Medicare.Models;

namespace Medicare.ViewModel
{
    public class AppoinmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}