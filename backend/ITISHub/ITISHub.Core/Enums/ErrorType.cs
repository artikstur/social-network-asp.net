namespace ITISHub.Core.Enums;

public enum ErrorType
{
    ValidationError = 1,
    AuthenticationError = 2,
    AuthorizationError = 3,
    NotFound = 4,
    Conflict = 5,
    ServerError = 6,
    BadRequest = 7,
}