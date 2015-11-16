using System.ComponentModel.DataAnnotations;

namespace Airport.MVC.Models
{
    public class AirlineModel
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }

    }
}
