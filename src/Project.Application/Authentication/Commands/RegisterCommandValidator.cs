using FluentValidation;

namespace Project.Application.Authentication.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator ()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .NotNull().WithMessage("First Name is required");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .NotNull().WithMessage("Last Name is required");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is not valid");
    }
}