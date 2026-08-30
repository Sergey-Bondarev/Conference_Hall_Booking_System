namespace Conference_Hall_Booking_System.Application.DTOs;

/// <summary>
/// Model representing a request to create a new room with its name, capacity, and base price per hour.
/// </summary>
public record CreateRoomRequest(string Name, int Capacity, decimal BasePricePerHour, List<AmenityRequest> Amenities);

/// <summary>
/// Model representing a request to update an existing room with its name, capacity, and base price per hour.
/// </summary>
public record UpdateRoomRequest(string Name, int Capacity, decimal BasePricePerHour, List<AmenityRequest> Amenities);

/// <summary>
/// Model representing a request to book a room with its ID, start time, end time, and a list of requested amenities.
/// </summary>
public record BookRoomRequest(Guid RoomId, DateTime StartTime, DateTime EndTime, List<string> Amenities);

/// <summary>
/// Model representing a response after booking a room, including the booking ID, room ID, total price, and a message indicating the result of the booking operation.
/// </summary>
public record BookingResponse(Guid BookingId, Guid RoomId, decimal TotalPrice, string Message);

/// <summary>
/// Internal model representing a request to create or update an amenity with its name and price.    
/// </summary>
public record AmenityRequest(string Name, decimal Price);