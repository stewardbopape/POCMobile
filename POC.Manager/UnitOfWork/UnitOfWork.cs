using POC.BusinessObjects;
using POC.IManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Manager
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContextTransaction _dbContextTransaction;
        private POCDBEntities _context;
           
        private bool disposed = false;
        IUserManager _userManager;
        IInformationManager _informationManager;

        public UnitOfWork()
        {
            this._context = new POCDBEntities();
            this._context.Configuration.LazyLoadingEnabled = false; //This should kept false for faster loading
            this._context.Configuration.ProxyCreationEnabled = false;
        }

        public IUserManager UserManager

        {
            get
            {
                return _userManager ?? (this._userManager = new UserManager(_context));
            }
        }

        public IInformationManager InformationManager
        {
            get
            {
                return _informationManager ?? (this._informationManager = new InformationManager(_context));
            }
        }

        public void BeginTransaction()
        {
            if (_dbContextTransaction == null)
            {
                this._dbContextTransaction = this._context.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _dbContextTransaction.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                _dbContextTransaction.Rollback();
                var exceptionMessage = ExceptionHaddler(ex);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }



        public void RollbackTransaction()
        {
            try
            {
                _dbContextTransaction.Rollback();
            }
            catch { }
        }

        public int SaveChanges()
        {
            try
            {
                return this._context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var exceptionMessage = ExceptionHaddler(ex);
                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        private string ExceptionHaddler(DbEntityValidationException ex)
        {
            // Retrieve the error messages as a list of strings.
            var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            // Join the list to a single string.
            var fullErrorMessage = string.Join("; ", errorMessages);
            // Combine the original exception message with the new one.
            return string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
        }


        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_context.Dispose();

                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}