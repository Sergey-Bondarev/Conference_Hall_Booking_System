using Conference_Hall_Booking_System.Application.Interfaces;
using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.Application.Services
{
    public class BookingService(IBookingRepository bookingRepository, 
                                IRoomRepository roomRepository,
                                PricingService pricingService)
    {
        public async Task<Booking> BookRoomAsync(Guid roomId, TimeRange requestedPeriod, List<string> amenityNames)
        {
            var room = await roomRepository.GetByIdAsync(roomId)
                ?? throw new Exception("Conference room not found.");

            var existingBookings = await bookingRepository.GetByRoomIdAsync(roomId);

            if (existingBookings.Any(b => b.Period.OverlapsWith(requestedPeriod)))
            {
                throw new Exception("Conference room is already booked for the selected time.");
            }

            var selectedAmenities = room.AvailableAmenities
                .Where(a => amenityNames.Contains(a.Name))
                .ToList();

            var totalPrice = pricingService.CalculateTotalPrice(room.BasePricePerHour, requestedPeriod, selectedAmenities);

            var booking = new Booking
            {
                RoomId = roomId,
                Period = requestedPeriod,
                SelectedAmenities = selectedAmenities,
                TotalPrice = totalPrice
            };

            await bookingRepository.AddAsync(booking);
            return booking;
        }

    }
}
