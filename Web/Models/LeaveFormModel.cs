using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class LeaveFormModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
        public string Note { get; set; }
        [Required]
        public string Attachement { get; set; }
        [Required]
        public string Institution { get; set; }
        [Required]
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; }
        public string UserID { get; set; }
        public int LeaveID { get; set; }
        [Display(Name = "State")]
        public int LeaveStateID { get; set; }
    }
}