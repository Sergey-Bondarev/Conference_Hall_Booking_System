namespace Conference_Hall_Booking_System.Domain
{
    public class TimeRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSpan Duration => End - Start;

        public TimeRange(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new ArgumentException("Start time should be earlier than end time.");

            Start = start;
            End = end;
        }

        public bool OverlapsWith(TimeRange other)
        {
            return Start < other.End && other.Start < End;
        }
    }
}
