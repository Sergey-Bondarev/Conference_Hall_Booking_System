using Conference_Hall_Booking_System.Application.Interfaces;

namespace Conference_Hall_Booking_System.Application.Services
{
    public class AnalyticsService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
    {
        public async Task<object> GetRevenueReportAsync(DateTime startDate, DateTime endDate)
        {
            var allBookings = await bookingRepository.GetAllAsync();
            var completedBookings = allBookings
                .Where(b => b.Period.Start >= startDate && b.Period.End <= endDate)
                .ToList();

            var totalRevenue = completedBookings.Sum(b => b.TotalPrice);
            var bookingsCount = completedBookings.Count;

            return new
            {
                Period = $"{startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd}",
                TotalBookings = bookingsCount,
                TotalRevenue = totalRevenue
            };
        }

        public async Task<Dictionary<int, int>> GetHourlyUtilizationAsync(DateTime targetDate)
        {
            var allBookings = await bookingRepository.GetAllAsync();
            var dayBookings = allBookings
                .Where(b => b.Period.Start.Date == targetDate.Date)
                .ToList();

            var hourlyUtilization = new Dictionary<int, int>();
            for (int i = 0; i < 24; i++)
            {
                hourlyUtilization[i] = 0;
            }

            foreach (var booking in dayBookings)
            {
                for (int hour = booking.Period.Start.Hour; hour < booking.Period.End.Hour; hour++)
                {
                    hourlyUtilization[hour]++;
                }
            }

            return hourlyUtilization;
        }
    }
}
