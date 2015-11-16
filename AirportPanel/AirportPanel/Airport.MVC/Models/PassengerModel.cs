using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.MVC.Models.Enums;

namespace Airport.MVC.Models
{
    public class PassengerModel
    {
        public int Id { get; set; }
        [Required]
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        [MaxLength(150)]
        [Required]
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

    }
}
