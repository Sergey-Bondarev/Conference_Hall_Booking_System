using Conference_Hall_Booking_System.Domain;

namespace ConferenceBookingApi.Application.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetByRoomIdAsync(Guid roomId);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task AddAsync(Booking booking);
}