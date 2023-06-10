using FluentValidation;

namespace ECommerce.Api.Dtos.Validators;

public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
{
    public RegisterUserRequestDtoValidator()
    {
        RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Enter a valid Email format");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Must have First Name");
        RuleFor(x => x.Username).NotEmpty().MinimumLength(4).WithMessage("Username must be at least 4 characters");
        RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password must be at least 5 character");

    }
    
}