using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.IManager
{
    public interface IUnitOfWork:IDisposable
    {

        int SaveChanges();
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        IUserManager UserManager { get; }
        IInformationManager InformationManager { get; }
    }
}
