using POC.BusinessObjects;
using POC.IManager;
using POC.IProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Provider
{
    public class InformationProvider : IInformationProvider
    {
        private IUnitOfWork _unitOfWork;

        public InformationProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public ResultObj<INFORMATION> AddInformation(INFORMATION information)
        {
            ResultObj<INFORMATION> result = new ResultObj<INFORMATION>() { isSuccessful = false };

            using (this._unitOfWork)
            {
                var added=this._unitOfWork.InformationManager.AddInformation(information);
                this._unitOfWork.SaveChanges();

                result.Data = added;
                result.isSuccessful = true;
            }
            return result;
        }

        public ResultObj<INFORMATION> GetInformationByCustomerId(string idNumber)
        {
            ResultObj<INFORMATION> result = new ResultObj<INFORMATION>() { isSuccessful = false };
            var info = this._unitOfWork.InformationManager.GetInformationByCustomerId(idNumber);
            result.Data = info;
            result.isSuccessful = true;
            return result;
        }

        public ResultObj<INFORMATION> GetInformationByMeterNo(string meterNumber)
        {
            ResultObj<INFORMATION> result = new ResultObj<INFORMATION>() { isSuccessful = false };
            var info = this._unitOfWork.InformationManager.GetInformationByMeterNo(meterNumber);

            result.Data = info;
            result.isSuccessful = true;
            return result;
        }

        public ResultObj<List<INFORMATION>> GetInformations()
        {
            ResultObj<List<INFORMATION>> result = new ResultObj<List<INFORMATION>>() { isSuccessful = false };
            var infos = this._unitOfWork.InformationManager.GetInformations();

            result.Data = infos;
            result.isSuccessful = true;
            return result;
        }
    }
}
