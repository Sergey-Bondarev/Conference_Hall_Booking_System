using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.Application.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetByRoomIdAsync(Guid roomId);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task AddAsync(Booking booking);
}