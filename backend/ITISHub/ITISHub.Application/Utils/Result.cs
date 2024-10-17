using ITISHub.Core.Enums;

namespace ITISHub.Application.Utils;

public class Error(string errorMessage, ErrorType errorType)
{
    public string ErrorMessage { get; set; } = errorMessage;
    public ErrorType ErrorType { get; set; } = errorType;
}

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, null);
    public static Result Failure(Error error) => new Result(false, error);
}


public class Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public Error Error { get; }

    private Result(T value, bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, null);

    public static Result<T> Failure(Error error) => new Result<T>(default, false, error);
}
