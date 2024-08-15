using Project.Application.Authentication.Common;

namespace Project.Application.Authentication.Queries;

public class LoginQueryHandler: IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public Task<ErrorOr<AuthenticationResult>> Handle (LoginQuery request, CancellationToken cancellationToken)
    {
        //TODO:
        throw new NotImplementedException();
    }
}
