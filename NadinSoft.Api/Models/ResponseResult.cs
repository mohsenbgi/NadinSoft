using NadinSoft.Domain.Common;
using Newtonsoft.Json.Linq;

namespace NadinSoft.Api.Models
{
    public class ResponseResult
    {
        private ResponseResult()
        {
            Status = 200;
        }

        public static implicit operator ResponseResult(Result result)
        {
            if(result.IsFailure) return Failure(result);

            if(result.GetType().IsGenericType)
            {
                var method = typeof(ResponseResult).GetMethod("Success");
                var generic = method.MakeGenericMethod(result.GetType().GenericTypeArguments);
                var response = generic.Invoke(null, new object[] { result });
                return (ResponseResult)response;
            }

            return Success(result.Message);
        }

        public static ResponseResult Success(string message)
        {
            return new ResponseResult
            {
                Message = message,
                IsSuccess = true,
                Status = 200
            };
        }

        public static ResponseResult Success(string message, object data)
        {
            return new ResponseResult
            {
                Message = message,
                IsSuccess = true,
                Data = data,
                Status = 200
            };
        }

        public static ResponseResult Success<T>(Result<T> result)
        {
            return new ResponseResult
            {
                Message = result.Message,
                IsSuccess = true,
                Data = result.Value,
                Status = 200
            };
        }

        public static ResponseResult Success()
        {
            return new ResponseResult
            {
                IsSuccess = true,
                Status = 200
            };
        }

        public static ResponseResult Failure(Result result)
        {
            return result switch
            {
                IValidationResult validationResult => new ResponseResult
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Status = 400,
                    Errors = validationResult.Errors
                },
                _ => new ResponseResult
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Status = 400
                }
            };
        }

        public static ResponseResult Failure(string message)
        {
            return new ResponseResult
            {
                Message = message,
                IsSuccess = false,
                Status = 400
            };
        }

        public static ResponseResult UnAuthorized(string message)
        {
            return new ResponseResult
            {
                Message = message,
                IsSuccess = false,
                Status = 401
            };
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public int Status { get; private set; }
        public string[] Errors { get; private set; }
    }
}
