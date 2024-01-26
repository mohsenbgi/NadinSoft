namespace NadinSoft.Api.Models
{
    public class ResponseResult
    {
        private ResponseResult()
        {
            Status = 200;
        }

        public ResponseResult(bool isSuccess, string message, object data) : this()
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public ResponseResult(bool isSuccess, string message) : this()
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public ResponseResult(bool isSuccess) : this()
        {
            IsSuccess = isSuccess;
        }

        public static ResponseResult Success(string message)
        {
            return new(true, message);
        }

        public static ResponseResult Success(string message, object data)
        {
            return new(true, message, data);
        }

        public static ResponseResult Success()
        {
            return new(true, "");
        }

        public static ResponseResult Failure(string message)
        {
            return new(false, message);
        }

        public static ResponseResult UnAuthorized(string message)
        {
            ResponseResult result = new(false, message);
            result.Status = 401;

            return result;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Status { get; set; }
    }
}
