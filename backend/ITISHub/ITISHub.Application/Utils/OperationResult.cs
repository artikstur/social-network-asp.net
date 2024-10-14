namespace ITISHub.Application.Utils;

public class OperationResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }

    protected OperationResult(string errorMessage, bool success)
    {
        ErrorMessage = errorMessage;
        Success = success;
    }

    public static OperationResult SuccessResult()
    {
        return new OperationResult(string.Empty, true);
    }

    public static OperationResult FailureResult(string errorMessage)
    {
        return new OperationResult(errorMessage, false);
    }
}


public class OperationResult<T> : OperationResult
{
    public T? Data { get; set; }

    public static OperationResult<T> SuccessResult(T data)
    {
        return new OperationResult<T>(string.Empty, true, data);
    }

    public static OperationResult<T?> FailureResult(string errorMessage)
    {
        return new OperationResult<T?>(errorMessage, true, default);
    }

    protected OperationResult(string errorMessage, bool success, T? data) : base(errorMessage, success)
    {
        Data = data;
    }
}


