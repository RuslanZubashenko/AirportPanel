
namespace Airport.DAL.DataContext
{
    using System.Data.Entity;
    using Model.Entities;

    public class AirlineDbContext : DbContext
    {
        

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<FlightPlaces> FlightsPlaces { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FlightConfiguration());
        }

        public new void Dispose()
        {
            base.Dispose();
        }  
     }
}
