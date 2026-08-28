using Conference_Hall_Booking_System.Domain;

namespace Conference_Hall_Booking_System.Application.Services
{
    public class PricingService
    {
        public decimal CalculateTotalPrice(decimal basePricePerHour, TimeRange period, IEnumerable<Amenity> selectedAmenities)
        {
            decimal totalRoomPrice = 0;
            var current = period.Start;

            while (current < period.End)
            {
                var hour = current.Hour;
                decimal multiplier = 1.0m;

                if (hour >= 6 && hour < 9)
                    multiplier = 0.9m;
                else if (hour >= 12 && hour < 14)
                    multiplier = 1.15m;
                else if (hour >= 18 && hour < 23)
                    multiplier = 0.8m;

                totalRoomPrice += basePricePerHour * multiplier;
                current = current.AddHours(1);
            }

            decimal amenitiesPrice = selectedAmenities.Sum(a => a.Price);

            return totalRoomPrice + amenitiesPrice;
        }
    }
}
