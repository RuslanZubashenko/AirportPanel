
namespace Airport.UnitOfWork
{
    using System;
    using DAL;
    using DAL.DataContext;
    using DAL.Model.Entities;

    public class AirlineUnit : IDisposable
    {
        private AirlineDbContext context = new AirlineDbContext();
        private GenericRepository<Airline> airlineRepository;

        public GenericRepository<Airline> AirlineRepository
        {
            get
            {
                if(this.airlineRepository == null)
                {
                    this.airlineRepository = new GenericRepository<Airline>(context);
                }
                return airlineRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                context.Dispose();
            }
            disposed = disposing;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
