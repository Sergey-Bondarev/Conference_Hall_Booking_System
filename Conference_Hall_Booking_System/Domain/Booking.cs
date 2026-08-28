using Conference_Hall_Booking_System.Domain;

public class Booking
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid RoomId { get; init; }
    public TimeRange Period { get; init; } = default!;
    public List<Amenity> SelectedAmenities { get; init; } = new();

    public decimal TotalPrice { get; set; }
}
