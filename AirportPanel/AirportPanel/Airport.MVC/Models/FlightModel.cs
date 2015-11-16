using System;

namespace Airport.MVC.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class FlightModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Display(Name = "Flight number")]
        public string FlightNumber { get; set; }
        [Display(Name = "Airline")]
        public int AirlineId { get; set; }
        public virtual AirlineModel Airline { get; set; }
        [Display(Name = "Arrival port")]
        public int ArrivalPortId { get; set; }
        [Display(Name = "Arrival port")]
        public virtual PortModel ArrivalPort { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Arrival date")]
        public DateTime ArrivalDate { get; set; }
        [Display(Name = "Departure port")]
        public int DeparturePortId { get; set; }
        [Display(Name = "Departure port")]
        public virtual PortModel DeparturePort { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Departure date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DepartureDate { get; set; }
        [MaxLength(5)]
        public string Terminal { get; set; }

        public string Gate { get; set; }
        public int PlaceQty { get; set; }
        public FlightStatus Status { get; set; }

    }
}
