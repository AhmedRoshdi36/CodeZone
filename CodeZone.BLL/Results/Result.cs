namespace CodeZone.BLL.Results;
    public class Result
    {
        public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;

    public string? ErrorMessage { get; private set; } = null;
        
        private Result(bool isSuccess, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
        
        public static Result Success() => new Result(true, null);
        public static Result Failure(string errorMessage) => new Result(false, errorMessage);
    }

