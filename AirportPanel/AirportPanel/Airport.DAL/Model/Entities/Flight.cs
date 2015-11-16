namespace Airport.DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Enums;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class Flight
    /// </summary>
    public class Flight : IComparable<Flight>, IComparer<Flight>
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FlightNumber { get; set; }
        public int ArrivalPortId { get; set; }
        public Port ArrivalPort { get; set; }
        public int DeparturePortId { get; set; }
        public Port DeparturePort { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        [MaxLength(10)]
        public string Terminal { get; set; }
        [MaxLength(10)]
        public string Gate { get; set; }
        public FlightStatus Status { get; set; }
        public int AirlineId { get; set; }
        public virtual Airline Airline { get; set; }
        public int PlaceQty { get; set; }
        public virtual List<FlightPlaces> Places { get; set; }

        public override string ToString()
        {
            var flightString = new StringBuilder();
            flightString.Append("Flight Id:");
            flightString.Append(this.Id);
            flightString.Append("; Flight #:");
            flightString.Append(this.FlightNumber);
            flightString.Append("; Airline:");
            flightString.Append(this.Airline.ToString());
            flightString.Append("; Arrival port: ");
            flightString.Append(this.ArrivalPort.ToString());
            flightString.Append("; Arrival Date: ");
            flightString.Append(this.ArrivalDate.ToString());
            flightString.Append("; Departure Port: ");
            flightString.Append(this.DeparturePort.ToString());
            flightString.Append("; Departure Date: ");
            flightString.Append(this.DepartureDate.ToString());
            flightString.Append("; Terminal: ");
            flightString.Append(this.Terminal);
            flightString.Append("; Status: ");
            flightString.Append(this.Status);

            return flightString.ToString();
        }
        public bool Equals(Flight obj)
        {
            return this.Id.Equals(obj.Id);
        }

        public int CompareTo(Flight other)
        {
            if (other == null)
            { return 1; }
            if (this.Id > other.Id)
            {
                return -1;
            }
            if (this.Id == other.Id)
            {
                return 0;
            }
            return 1;
        }

        public int Compare(Flight x, Flight y)
        {
            if (x == null || y == null)
                return 1;
            if (x.Id > y.Id)
            {
                return 1;
            }
            if (x.Id == y.Id)
            {
                return 0;
            }
            return -1;
        }
    }
}
