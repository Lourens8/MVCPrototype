using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class LeaveList
    {
        public List<Leave> Leaves { get; set; }
        public List<PublicHoliday> Holidays { get; set; }
        public List<UserLeave> UserLeaves { get; set; }
        [JsonIgnore]
        public LeaveFormModel FormModel { get; set; }
    }
}