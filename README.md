# Conference_Hall_Booking_System

The .NET Minimal API application for managing conference room reservations. This API handles room management, pricing based on time of day, availability validation and business reports.

## Tech Stack

* **Framework:** .NET 10 (ASP.NET Core Minimal APIs)
* **Language:** C# 14
* **Validation:** FluentValidation
* **Documentation:** Swagger

## Features

* **Room Management:** CRUD operations for conference rooms with customizable amenities (e.g., Wi-Fi, Projector).
* **Booking System:** Automatic prevention of overlapping bookings using a custom `TimeRange` value object.
* **Dynamic Pricing Engine:** Calculates booking costs based on different time-of-day multipliers (morning discounts, peak hour markups, evening discounts).
* **Availability Search:** Find available rooms by specified date, time period, and minimum capacity.
* **Analytics & Reports:** Generate revenue reports and hourly utility usage heatmaps.
* **Robust Validation:** Request validation using `FluentValidation` integrated seamlessly via endpoint filters.

## Architecture & Project Structure

The project strictly follows a layered architecture to ensure separation of concerns, testability, and scalability.

* **`Domain`**
  Contains enterprise-wide business rules. No external dependencies.
  * Entities: `Room`, `Booking`
  * Records: `Amenity`
  * `TimeRange` encapsulates date validation and overlap logic (encapsulates date validation and overlap logic)

* **`Application`**
  Contains application-specific business rules.
  * DTOs: Data Transfer Objects for API requests and responses.
  * Interfaces: `IRoomRepository`, `IBookingRepository`.
  * Services: `BookingService`, `RoomService`, `PricingService`, `AnalyticsService`.
  * Validators: `FluentValidation` rules for incoming DTOs.

* **`Infrastructure`**
  Handles data persistence.
  * Database: `InMemoryDatabase` (singleton in-memory store simulating a real DB).
  * Repositories: `BookingRepository` and `RoomRepository` are implementations of the data access interfaces.

* **`API`**
  The entry point of the application.
  * Endpoints: Modular Minimal API definitions (`RoomEndpoints`, `BookingEndpoints`, `ReportEndpoints`).
  * Filters: `ValidationFilter<T>` to intercept and validate requests.
