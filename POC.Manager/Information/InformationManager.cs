using POC.BusinessObjects;
using POC.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Manager
{
    public class InformationManager : IInformationManager
    {
        POCDBEntities _context;

        public InformationManager(POCDBEntities context)
        {
            this._context = context;

        }
        public INFORMATION AddInformation(INFORMATION information)
        {
            return this._context.INFORMATION.Add(information);
        }

        public INFORMATION GetInformationByCustomerId(string idNumber)
        {
            return this._context.INFORMATION.Where(o => o.ID_NO.Trim() == idNumber.Trim()).SingleOrDefault();
        }

        public INFORMATION GetInformationByMeterNo(string meterNumber)
        {
            return this._context.INFORMATION.Where(o => o.METERNUMBER.Trim() == meterNumber.Trim()).SingleOrDefault();
        }

        public List<INFORMATION> GetInformations()
        {
            return this._context.INFORMATION.ToList();
        }
    }
}
