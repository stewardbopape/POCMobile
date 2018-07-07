using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC.BusinessObjects;

namespace POC.IProvider
{
    public interface IInformationProvider
    {
        ResultObj<INFORMATION> AddInformation(INFORMATION information);
        ResultObj<INFORMATION> GetInformationByMeterNo(string meterNumber);
        ResultObj<INFORMATION> GetInformationByCustomerId(string idNumber);
        ResultObj<List<INFORMATION>> GetInformations();
    }
}
