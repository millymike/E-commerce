using FluentValidation;

namespace ECommerce.Api.Dtos.Validators;

public class RegisterCustomerRequestDtoValidator : AbstractValidator<RegisterCustomerRequestDto>
{
    public RegisterCustomerRequestDtoValidator()
    {
        RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Enter a valid Email format");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.");
        RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password must be at least 5 character");
    }
}