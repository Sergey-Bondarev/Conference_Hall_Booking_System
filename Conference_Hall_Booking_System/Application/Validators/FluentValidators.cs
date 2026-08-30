using Conference_Hall_Booking_System.Application.DTOs;
using FluentValidation;

namespace Conference_Hall_Booking_System.Application.Validators;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Conference room name is required");
        RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Capacity must be a positive number");
        RuleFor(x => x.BasePricePerHour).GreaterThan(0).WithMessage("Base price per hour must be a positive number");

        RuleForEach(x => x.Amenities).SetValidator(new AmenityRequestValidator());
    }
}

public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequest>
{
    public UpdateRoomRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Capacity).GreaterThan(0);
        RuleFor(x => x.BasePricePerHour).GreaterThan(0);
        RuleForEach(x => x.Amenities).SetValidator(new AmenityRequestValidator());
    }
}

public class BookRoomRequestValidator : AbstractValidator<BookRoomRequest>
{
    public BookRoomRequestValidator()
    {
        RuleFor(x => x.RoomId).NotEmpty().WithMessage("Room ID is required");
        RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("Start time must be before end time");
        RuleFor(x => x.StartTime).GreaterThan(DateTime.UtcNow).WithMessage("Cannot book a room in the past");
    }
}

public class AmenityRequestValidator : AbstractValidator<AmenityRequest>
{
    public AmenityRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Amenity name is required");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Amenity price cannot be negative.");
    }
}