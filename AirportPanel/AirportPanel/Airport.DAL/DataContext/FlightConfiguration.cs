
namespace Airport.DAL.DataContext
{
    using System.Data.Entity.ModelConfiguration;
    using Model.Entities;

    class FlightConfiguration : EntityTypeConfiguration<Flight>
    {
        public FlightConfiguration()
        {
            Property(d => d.ArrivalPortId);
            HasRequired(s => s.ArrivalPort)
                .WithMany(l => l.ArrivalFlights)
                .HasForeignKey(s => s.ArrivalPortId)
                .WillCascadeOnDelete(false);
            HasRequired(s => s.DeparturePort)
                .WithMany(l => l.DeparturedFlights)
                .HasForeignKey(s => s.DeparturePortId)
                .WillCascadeOnDelete(false);
        }
    }
}
