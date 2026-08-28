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

            group.MapGet("/", async (RoomService service) =>
                Results.Ok(await service.GetAllRoomsAsync()));

            group.MapPost("/", async (CreateRoomRequest req, RoomService service) =>
            {
                var room = new Room { Name = req.Name, Capacity = req.Capacity, BasePricePerHour = req.BasePricePerHour };
                var id = await service.CreateRoomAsync(room);
                return Results.Created($"/api/rooms/{id}", new { Id = id });
            })
            .AddEndpointFilter<ValidationFilter<CreateRoomRequest>>();

            group.MapPut("/{id:guid}", async (Guid id, UpdateRoomRequest req, RoomService service) =>
            {
                var existingRoom = await service.GetRoomAsync(id);
                if (existingRoom is null) return Results.NotFound();

                existingRoom.Name = req.Name;
                existingRoom.Capacity = req.Capacity;
                existingRoom.BasePricePerHour = req.BasePricePerHour;

                await service.UpdateRoomAsync(existingRoom);
                return Results.NoContent();
            })
            .AddEndpointFilter<ValidationFilter<UpdateRoomRequest>>();

            group.MapDelete("/{id:guid}", async (Guid id, RoomService service) =>
            {
                await service.DeleteRoomAsync(id);
                return Results.NoContent();
            });
        }
    }
}
