using Conference_Hall_Booking_System.API.Endpoints;
using Conference_Hall_Booking_System.Application.Interfaces;
using Conference_Hall_Booking_System.Application.Services;
using Conference_Hall_Booking_System.Application.Validators;
using Conference_Hall_Booking_System.Infrastructure;
using Conference_Hall_Booking_System.Infrastructure.Repositories;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreateRoomRequestValidator>();
builder.Services.AddSingleton<InMemoryDatabase>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddSingleton<PricingService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<BookingService>();

builder.Services.AddScoped<AnalyticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRoomEndpoints();
app.MapBookingEndpoints();
app.MapReportEndpoints();

app.UseHttpsRedirection();
app.Run();
