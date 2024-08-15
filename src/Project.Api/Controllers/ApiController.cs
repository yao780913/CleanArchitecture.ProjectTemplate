using System.IdentityModel.Tokens.Jwt;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Project.Api.Controllers;

[ApiController]
public class ApiController: ControllerBase
{
    protected JwtSecurityToken ReadToken(HttpRequest request)
    {
        var authorization = request.Headers.Authorization.ToString().Split("Bearer")[1].Trim();
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(authorization);
    }
    
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }
    
    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Validation   => StatusCodes.Status400BadRequest,
            ErrorType.Failure      => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            ErrorType.NotFound     => StatusCodes.Status404NotFound,
            ErrorType.Conflict     => StatusCodes.Status409Conflict,
            _                      => StatusCodes.Status500InternalServerError,
        };
        
        return Problem(statusCode: statusCode, detail: error.Description);
    }
    
    protected IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }
        
        return ValidationProblem(modelStateDictionary);
    }
}