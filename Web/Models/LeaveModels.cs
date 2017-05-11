using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Leave
    {
        [Key]
        public int LeaveID { get; set; }
        public LeaveState State { get; set; }
        public int LeaveStateID { get; set; }
        public string UserID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
        public string Note { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        [NotMapped]
        public decimal RunningTotal { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public decimal Amount { get; set; }
        [NotMapped]
        public bool Expires { get; set; }
        [NotMapped]
        public string TypeName { get; set; }
        [NotMapped]
        public bool IsFuture { get; set; }
        [NotMapped]
        public bool IsCurrent { get; set; }
        [NotMapped]
        public string Name { get; set; }
    }

    public class LeaveState
    {
        [Key]
        public int LeaveStateID { get; set; }
        public string Description { get; set; }
    }

    public class AnnualLeave : Leave
    {
    }

    public class MedicalLeave : Leave
    {
        public string Attachement { get; set; }
    }

    public class StudyLeave : Leave
    {
        public string StudentNumber { get; set; }
        public string Institution { get; set; }
    }

    public class PublicHoliday
    {
        [Key]
        public int PublicHolidayID { get; set; }
        public string Name { get; set; }
        public DateTime HolidayDate { get; set; }
    }

    public class UserLeave
    {
        public List<Leave> Leaves { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public DateTime EmploymentStartDate { get; set; }
    }
}