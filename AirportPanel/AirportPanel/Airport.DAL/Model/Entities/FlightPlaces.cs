namespace Airport.DAL.Model.Entities
{
    using Enums;

    public class FlightPlaces
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
        public PlaceClass PlaceClass { get; set; }
        public int Quantity { get; set; }

    }
}
