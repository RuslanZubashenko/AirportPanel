namespace Airport.DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Airline : IComparable<Airline>, IComparer<Airline>
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public virtual List<Flight> Flights { get; set; }

        public override string ToString() => (this.Name + " (" + this.Id + ")");

        public int Compare(Airline x, Airline y)
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

        public int CompareTo(Airline other)
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
    }
}
