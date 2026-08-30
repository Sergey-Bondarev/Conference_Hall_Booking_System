using Conference_Hall_Booking_System.Application.Services;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/reports").WithTags("Reports");

        group.MapGet("/revenue", async (DateTime start, DateTime end, AnalyticsService service) =>
            Results.Ok(await service.GetRevenueReportAsync(start, end)))
            .WithSummary("Get Revenue Report")
            .WithDescription("Retrieves a report of the total revenue generated within the specified date range.");

        group.MapGet("/utilization", async (DateTime date, AnalyticsService service) =>
            Results.Ok(await service.GetHourlyUtilizationAsync(date)))
            .WithSummary("Get Hourly Utilization Usage Report")
            .WithDescription("Retrieves a report of the hourly utilization of conference rooms for the specified date.");
    }
}