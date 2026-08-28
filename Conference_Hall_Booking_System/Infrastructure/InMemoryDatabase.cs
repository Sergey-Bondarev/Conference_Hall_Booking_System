using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.Infrastructure
{
    public class InMemoryDatabase
    {
        public List<Room> Rooms { get; } = new();
        public List<Booking> Bookings { get; } = new();

        public InMemoryDatabase()
        {
            var commonAmenities = new List<Amenity>
        {
            new Amenity("Проєктор", 500),
            new Amenity("Wi-Fi", 300),
            new Amenity("Звук", 700)
        };

            Rooms.Add(new Room
            {
                Name = "Зал А",
                Capacity = 50,
                BasePricePerHour = 2000,
                AvailableAmenities = new List<Amenity>(commonAmenities)
            });

            Rooms.Add(new Room
            {
                Name = "Зал B",
                Capacity = 100,
                BasePricePerHour = 3500,
                AvailableAmenities = new List<Amenity>(commonAmenities)
            });

            Rooms.Add(new Room
            {
                Name = "Зал C",
                Capacity = 30,
                BasePricePerHour = 1500,
                AvailableAmenities = new List<Amenity>(commonAmenities)
            });
        }
    }
}
