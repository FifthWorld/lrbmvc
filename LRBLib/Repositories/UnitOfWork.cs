using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Repositories
{
    public class UnitOfWork  : IDisposable
    {
        private LandsContext _context = new LandsContext();
        private LandApplicationRepository _landApplicationRepository;

        public UnitOfWork()
        {
            _landApplicationRepository = new LandApplicationRepository(_context);
        }

        public LandApplicationRepository LandApplicationRepository
        {
            get { return _landApplicationRepository; }
        }

        public int Save()
         {
            return _context.SaveChanges();
         }

        public LandsContext Context
        {
            
            get{
                return _context;
            }
        }

       private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
