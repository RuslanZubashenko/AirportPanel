
namespace Airport.DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Enums;

    public class Passenger : IComparable<Passenger>, IComparer<Passenger>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
        [MaxLength(150)]
        [Required]
        [Display(ShortName = "Фамилия")]
        public string FirstName { get; set; }
        [MaxLength(150)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Nationality { get; set; }
        [MaxLength(1024)]
        public string Passport { get; set; }
        public DateTime DateOfBirthday { get; set; }
        public Sex Sex { get; set; }
        public PlaceClass PlaceClass { get; set; }


        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Id: ");
            stringBuilder.Append(this.Id);
            stringBuilder.Append("; Flight number: ");
            stringBuilder.Append(this.Flight.FlightNumber);
            stringBuilder.Append("; First name: ");
            stringBuilder.Append(this.FirstName);
            stringBuilder.Append("; Last name");
            stringBuilder.Append(this.LastName);
            stringBuilder.Append("; Nationality: ");
            stringBuilder.Append(this.Nationality);
            stringBuilder.Append("; Passport");
            stringBuilder.Append(this.Passport);
            stringBuilder.Append("; Date of Birth:");
            stringBuilder.Append(this.DateOfBirthday.ToShortDateString());
            stringBuilder.Append("; Sex: ");
            stringBuilder.Append(this.Sex.ToString());
            stringBuilder.Append("; Class:");
            stringBuilder.Append(this.PlaceClass.ToString());

            return stringBuilder.ToString();
        }

        public bool Equals(Passenger obj)
        {
            return this.Id.Equals(obj.Id);
        }

        public int Compare(Passenger x, Passenger y)
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

        public int CompareTo(Passenger other)
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
