namespace Conference_Hall_Booking_System.Application.DTOs;

public record CreateRoomRequest(string Name, int Capacity, decimal BasePricePerHour);

public record UpdateRoomRequest(string Name, int Capacity, decimal BasePricePerHour);

public record BookRoomRequest(Guid RoomId, DateTime StartTime, DateTime EndTime, List<string> Amenities);

public record BookingResponse(Guid BookingId, Guid RoomId, decimal TotalPrice, string Message);