using Project.Application.Authentication.Common;

namespace Project.Application.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public Task<ErrorOr<AuthenticationResult>> Handle (RegisterCommand request, CancellationToken cancellationToken)
    {
        //TODO:
        throw new NotImplementedException();
    }
}