using Project.Application.Authentication.Common;

namespace Project.Application.Authentication.Queries;

public record LoginQuery (string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
