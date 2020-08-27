using System;
using System.Threading.Tasks;

namespace HCAM.Common.Wrappers
{
    public class Result<T>
    {
        public T Value { get; set; }
        public string Error { get; }
        public bool Success => Error == null;
        public bool Failure => !Success;

        protected Result (T value)
        {
            Value = value;
        }
        protected Result(string message)
        {
            Error = message;
        }
        protected Result(T value, string message)
        {
            Value = value;
            Error = message;
        }

        public static Result<TF> Fail <TF>(string message) => new Result<TF>(default(TF), message);
        public static Task<Result<T>> FailAsync(string message) => Task.FromResult(new Result<T>(default(T), message));
        public static Result<T> Ok(T value) => new Result<T>(value);
        public static Task<Result<T>> OkAsync(T value) => Task.FromResult(new Result<T>(value));
    }
}
