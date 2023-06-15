using FluentValidation;

namespace ECommerce.Api.Dtos.Validators;

public class LoginCustomerRequestDtoValidator : AbstractValidator<LoginCustomerRequestDto>
{
    public LoginCustomerRequestDtoValidator()
    {
        RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Enter a valid Email format");
        RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password must be at least 5 characters");
    }
    
}