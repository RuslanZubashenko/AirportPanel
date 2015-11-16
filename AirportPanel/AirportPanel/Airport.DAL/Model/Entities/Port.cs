
namespace Airport.DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Port : IComparable<Port>, IComparer<Port>

    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public virtual List<Flight> DeparturedFlights { get; set; }
        public virtual List<Flight> ArrivalFlights { get; set; }


        public override string ToString() => (this.Name + " (" + this.Id + ")");

        public bool Equals(Port obj)
        {
            return this.Id.Equals(obj.Id);
        }

        public int CompareTo(Port other)
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

        public int Compare(Port x, Port y)
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
