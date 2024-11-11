using eCommerce.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerce.Application.Validations.Authentication
{
    public class LoginUserValidator : AbstractValidator<LoginUser>
    {

        public LoginUserValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
