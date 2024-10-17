using ITISHub.Application.Utils;
using ITISHub.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ITISHub.Application.Services;

public class ErrorResponseFactory
{
    public IActionResult CreateResponse(Error error)
    {
        switch (error.ErrorType)
        {
            case ErrorType.AuthenticationError:
                return new UnauthorizedObjectResult(error.ErrorMessage);
            case ErrorType.ValidationError:
                return new BadRequestObjectResult(error.ErrorMessage);
            case ErrorType.AuthorizationError:
                return new ForbidResult();
            case ErrorType.NotFound:
                return new NotFoundObjectResult(error.ErrorMessage);
            case ErrorType.Conflict:
                return new ConflictObjectResult(error.ErrorMessage);
            case ErrorType.ServerError:
                return new StatusCodeResult(500); 
            case ErrorType.BadRequest:
                return new BadRequestObjectResult(error.ErrorMessage);
            default:
                return new StatusCodeResult(500); 
        }
    }
}