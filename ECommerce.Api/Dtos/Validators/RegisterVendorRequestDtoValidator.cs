using FluentValidation;

namespace ECommerce.Api.Dtos.Validators;

public class RegisterVendorRequestDtoValidator : AbstractValidator<RegisterVendorRequestDto>
{
    public RegisterVendorRequestDtoValidator()
    {
        RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Enter a valid Email format");
        RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company Name is required");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.").Matches(@"^\+?\d{1,3}?[-.\s]?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$").WithMessage("Invalid phone number.");
        RuleFor(x => x.Description).NotEmpty().MinimumLength(20).WithMessage("Description must be at least 20 characters");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        RuleFor(x => x.TaxIdentificationNumber).NotEmpty().WithMessage("Tax identification number is required.");
        RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password must be at least 5 character");

    }
    
}