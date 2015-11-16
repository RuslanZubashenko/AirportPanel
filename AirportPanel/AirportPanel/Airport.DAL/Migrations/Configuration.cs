namespace Airport.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Model.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Airport.DAL.DataContext.AirlineDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Airport.DAL.DataContext.AirlineDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Flights.AddOrUpdate(
                p => p.Id,
                new Model.Entities.Flight
                {
                    Id = 1,
                    FlightNumber = "FN 20212",
                    AirlineId = 1,
                    Airline = new Model.Entities.Airline
                    {
                        Id = 1,
                        Name = "Air Ukraine"
                    },
                    ArrivalDate = DateTime.Parse("2015/11/10 15:25"),
                    ArrivalPort = new Model.Entities.Port
                    {
                        Id = 1,
                        Name = "Kyiv Boryspil"
                    },
                    ArrivalPortId = 1,
                    DepartureDate = DateTime.Parse("2015/11/10 18:30"),
                    DeparturePort = new Model.Entities.Port
                    {
                        Id = 2,
                        Name = "New York JFK"
                    },
                    DeparturePortId = 2,
                    Gate = "5",
                    Terminal = "D",
                    Status = Model.Enums.FlightStatus.ChekIn,
                    PlaceQty = 250,
                    Places = new System.Collections.Generic.List<FlightPlaces>
                    {
                        new FlightPlaces
                        {
                            Id = 1,
                            FlightId = 1,
                            PlaceClass = Model.Enums.PlaceClass.Business,
                            Quantity = 50
                        },
                        new FlightPlaces
                        {
                            Id = 2,
                            FlightId = 1,
                            PlaceClass = Model.Enums.PlaceClass.Economy,
                            Quantity = 200
                        }
                    }
                });
        }
    }
}
