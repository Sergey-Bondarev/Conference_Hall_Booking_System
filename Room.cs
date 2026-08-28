using System;
namespace ConferenceBookingApi.Domain;

public class Room
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }

    public List<Amenity> AvailableAmenities { get; set; } = new();
    public Room()

	{

	}
}
