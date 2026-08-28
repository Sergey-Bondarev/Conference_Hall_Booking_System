using Conference_Hall_Booking_System.Application.Interfaces;

namespace Conference_Hall_Booking_System.Infrastructure.Repositories
{
    public class BookingRepository(InMemoryDatabase db) : IBookingRepository
    {
        public Task<IEnumerable<Booking>> GetByRoomIdAsync(Guid roomId) =>
            Task.FromResult(db.Bookings.Where(b => b.RoomId == roomId).AsEnumerable());

        public Task<IEnumerable<Booking>> GetAllAsync() =>
            Task.FromResult(db.Bookings.AsEnumerable());

        public Task AddAsync(Booking booking)
        {
            db.Bookings.Add(booking);
            return Task.CompletedTask;
        }
    }
}
