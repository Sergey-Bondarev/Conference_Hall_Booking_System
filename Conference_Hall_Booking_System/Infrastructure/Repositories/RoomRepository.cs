using Conference_Hall_Booking_System.Application.Interfaces;
using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.Infrastructure.Repositories
{
    public class RoomRepository(InMemoryDatabase db) : IRoomRepository
    {
        public Task<IEnumerable<Room>> GetAllAsync() =>
        Task.FromResult(db.Rooms.AsEnumerable());

        public Task<Room?> GetByIdAsync(Guid id) =>
            Task.FromResult(db.Rooms.FirstOrDefault(r => r.Id == id));

        public Task AddAsync(Room room)
        {
            db.Rooms.Add(room);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Room room)
        {
            var index = db.Rooms.FindIndex(r => r.Id == room.Id);
            if (index != -1)
            {
                db.Rooms[index] = room;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            db.Rooms.RemoveAll(r => r.Id == id);
            return Task.CompletedTask;
        }
    }
}
