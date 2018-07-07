using POC.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.IManager
{
    public interface IInformationManager
    {
        INFORMATION AddInformation(INFORMATION information);
        INFORMATION GetInformationByMeterNo(string meterNumber);
        INFORMATION GetInformationByCustomerId(string idNumber);
        List<INFORMATION> GetInformations();
    }
}
