using Conference_Hall_Booking_System.Application.Interfaces;
using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.Application.Services
{
    public class RoomService(IRoomRepository repository)
    {
        public Task<IEnumerable<Room>> GetAllRoomsAsync() => repository.GetAllAsync();

        public Task<Room?> GetRoomAsync(Guid id) => repository.GetByIdAsync(id);

        public async Task<Guid> CreateRoomAsync(Room room)
        {
            await repository.AddAsync(room);
            return room.Id;
        }

        public Task UpdateRoomAsync(Room room) => repository.UpdateAsync(room);

        public Task DeleteRoomAsync(Guid id) => repository.DeleteAsync(id);
    }
}
