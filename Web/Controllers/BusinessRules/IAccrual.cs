using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers.BusinessRules
{
    public interface IAccrual
    {
        List<Leave> AccrueFromEmploymentStartDate(DateTime employmentStartDate);
        Type Handles();
    }
}
