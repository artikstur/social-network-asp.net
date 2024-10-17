using ITISHub.Application.Utils;
using ITISHub.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITISHub.Application.Services;

public class ErrorResponseFactory
{
    public IActionResult CreateResponse(Error error)
    {
        var responseError = new ResponseError
        (
            ErrorCode: error.ErrorType.ToString(), 
            ErrorMessage: error.ErrorMessage,
            InvalidField: null
        );

        switch (error.ErrorType)
        {
            case ErrorType.AuthenticationError:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            case ErrorType.ValidationError:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            case ErrorType.AuthorizationError:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            case ErrorType.NotFound:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            case ErrorType.Conflict:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            case ErrorType.ServerError:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            case ErrorType.BadRequest:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            default:
                return new ObjectResult(Envelope.Error(new List<ResponseError> { responseError }))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
        }
    }
}
