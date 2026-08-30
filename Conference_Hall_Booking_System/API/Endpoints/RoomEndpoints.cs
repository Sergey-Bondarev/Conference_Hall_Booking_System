using Conference_Hall_Booking_System.API.Filters;
using Conference_Hall_Booking_System.Application.DTOs;
using Conference_Hall_Booking_System.Application.Services;
using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.API.Endpoints
{
    public static class RoomEndpoints
    {
        public static void MapRoomEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/rooms").WithTags("Rooms");

            group.MapGet("/all", async (RoomService service) =>
                Results.Ok(await service.GetAllRoomsAsync())).WithSummary("Get All Rooms")
                .WithDescription("Returns a list of all rooms in the system.");

            group.MapGet("/available", async (DateTime start, DateTime end, int capacity, BookingService bookingService) =>
            {
                if (start >= end)
                {
                    return Results.BadRequest(new { Error = "Start time must be before end time." });
                }

                var availableRooms = await bookingService.GetAvailableRoomsAsync(start, end, capacity);
                return Results.Ok(availableRooms);
            }).WithSummary("Search for Available Rooms")
              .WithDescription("Returns a list of rooms that can accommodate the specified number of people and have no existing bookings within the given time range.");

            group.MapPost("/create", async (CreateRoomRequest req, RoomService service) =>
            {
                var room = new Room { Name = req.Name, Capacity = req.Capacity, BasePricePerHour = req.BasePricePerHour,
                AvailableAmenities = req.Amenities.Select(a => new Amenity(a.Name, a.Price)).ToList()};
                var id = await service.CreateRoomAsync(room);
                return Results.Created($"/api/rooms/{id}", new { Id = id });
            })
            .AddEndpointFilter<ValidationFilter<CreateRoomRequest>>()
            .WithSummary("Create New Room")
            .WithDescription("Creates a new room with the specified name, capacity, and base price per hour.");

            group.MapPut("/update/{id:guid}", async (Guid id, UpdateRoomRequest req, RoomService service) =>
            {
                var existingRoom = await service.GetRoomAsync(id);
                if (existingRoom is null) return Results.NotFound();

                existingRoom.Name = req.Name;
                existingRoom.Capacity = req.Capacity;
                existingRoom.BasePricePerHour = req.BasePricePerHour;
                existingRoom.AvailableAmenities = req.Amenities.Select(a => new Amenity(a.Name, a.Price)).ToList();

                await service.UpdateRoomAsync(existingRoom);
                return Results.NoContent();
            })
            .AddEndpointFilter<ValidationFilter<UpdateRoomRequest>>()
            .WithSummary("Update Room")
            .WithDescription("Updates an existing room with the specified name, capacity, and base price per hour.");

            group.MapDelete("/delete/{id:guid}", async (Guid id, RoomService service) =>
            {
                await service.DeleteRoomAsync(id);
                return Results.NoContent();
            }).WithSummary("Delete Room")
              .WithDescription("Deletes an existing room from the system.");
        }
    }
}
