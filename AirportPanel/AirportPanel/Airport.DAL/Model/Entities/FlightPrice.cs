
namespace Airport.DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class FlightPrice : IComparable<FlightPrice>, IComparer<FlightPrice>
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
        public PlaceClass PlaceClass { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:x.xx")]
        public decimal Price { get; set; }


        public override string ToString() =>
            (this.Id.ToString() + ". " + this.Flight.FlightNumber + " " + this.PlaceClass.ToString() + " "
            + this.Price.ToString());

        public bool Equals(FlightPrice obj)
        {
            return this.Id.Equals(obj.Id);
        }

        public int CompareTo(FlightPrice other)
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

        public int Compare(FlightPrice x, FlightPrice y)
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
