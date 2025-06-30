using AutoMapper;
using ED.Result;
using FluentValidation;
using MacropadServer.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MacropadServer.Application.Auth;
public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<Result<string>>;


internal sealed class RegisterCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper) : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        bool isEmailExists = await userManager.Users.AnyAsync(a => a.Email == request.Email, cancellationToken);
        if (isEmailExists)
        {
            return Result<string>.Failure("Email already exists");
        }

        //AppUser user = mapper.Map<AppUser>(request);
        AppUser user = request.Adapt<AppUser>();
        user.UserName = request.Email;
        user.CreatedDate = DateTime.UtcNow;
        //user.EmailConfirmed = true;

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure(result.Errors.FirstOrDefault()?.Description ?? "Unknown error");
        }

        return Result<string>.Succeed("User created successful");
    }
}


public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(2, 100)
            .WithMessage("First name must be between 2 and 100 characters.");

        RuleFor(r => r.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .Length(2, 200)
            .WithMessage("Last name must be between 2 and 200 characters.");

        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .Length(5, 300)
            .WithMessage("Email must be between 5 and 100 characters.");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");

    }
}