namespace LuckysDepartmentStore.Utilities
{
    public class ExecutionResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ExecutionResult()
        {
        }

        public ExecutionResult(bool isSuccess, T data, string errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static ExecutionResult<T> Success(T data)
        {
            return new ExecutionResult<T>(true, data, null);
        }

        public static ExecutionResult<T> Failure(string errorMessage)
        {
            return new ExecutionResult<T>(false, default(T), errorMessage);
        }
    }

}
