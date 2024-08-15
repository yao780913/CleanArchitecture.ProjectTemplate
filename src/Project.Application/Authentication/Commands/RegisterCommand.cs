using Project.Application.Authentication.Common;

namespace Project.Application.Authentication.Commands;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;