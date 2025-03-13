using FluentValidation;
using TeeMugShop.Application.Features.Accounts.Commands;

namespace TeeMugShop.Application.Accounts.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.NIF).NotEmpty().Length(9);
            RuleFor(x => x.Phone).NotEmpty().Length(9, 15);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
