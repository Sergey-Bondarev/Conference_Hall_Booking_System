using Conference_Hall_Booking_System.API.Filters;
using Conference_Hall_Booking_System.Application.DTOs;
using Conference_Hall_Booking_System.Application.Services;
using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.API.Endpoints
{
    public static class BookingEndpoints
    {
        public static void MapBookingEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/bookings").WithTags("Bookings");

            group.MapPost("/add", async (BookRoomRequest req, BookingService service) =>
            {
                try
                {
                    var timeRange = new TimeRange(req.StartTime, req.EndTime);
                    var booking = await service.BookRoomAsync(req.RoomId, timeRange, req.Amenities);

                    var response = new BookingResponse(booking.Id, booking.RoomId, booking.TotalPrice, "Booking successful");
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Error = ex.Message });
                }
            })
            .AddEndpointFilter<ValidationFilter<BookRoomRequest>>()
            .WithSummary("Create New Booking")
            .WithDescription("Checks the availability of the room during the specified time, considers all existing bookings to avoid conflicts. Also dynamically calculates the total cost considering peak, standard, and evening hours, as well as the cost of selected additional services.");
        }
    }
}
