using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Wrappers;

namespace HCAM.Common.Extensions
{
    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this T result) 
            => result == null
            ? Result<T>.Fail<T>("Data not Found or potentially bad request")
            : Result<T>.Ok(result);
        public static Result<T> ToResult<T>(this Result<T> result)
            => result == null
                ? Result<T>.Fail<T>("Data not Found or potentially bad request")
                : (result.Success ? result : Result<T>.Fail<T>(result.Error));

        public static T Unbind<T>(this Result<T> result) 
            => result.Value;

        public static Result<T> Bind<T> (this Result<T> result, Func<T, Result<T>> func) 
            => result == null
            ? Result<T>.Fail<T>("Data not Found or potentially bad request")
            : (result.Success ? func(result.Value) : Result<T>.Fail<T>(result.Error));

        public static Result<TSuccessNew> Bind<TSuccess, TSuccessNew>(this Result<TSuccess> result,
            Func<TSuccess, Result<TSuccessNew>> f) 
            => result == null
            ? Result<TSuccessNew>.Fail<TSuccessNew>("Data not Found")
            : (result.Success
                ? f(result.Value)
                : Result<TSuccessNew>.Fail<TSuccessNew>(result.Error));

        public static Result<T> HandleResult<T> (this Result<T> result, Func<Result<T>, Result<T>> f = null) 
            => result.Failure 
            ? Result<T>.Fail<T>(result.Error) 
            : f == null 
                ? result
                : f(result);

        public static async Task<Result<IEnumerable<T>>> TaskAsyncResponse<T, TR>(this Result<Task<IEnumerable<TR>>> result)
        {
            if (!result.Success)
                return Result<IEnumerable<T>>.Fail<IEnumerable<T>>(result.Error);
            var items = await result.Value;
            return Result<IEnumerable<T>>.Ok(items.EnumerableTo<T>());
        }
        public static async Task<Result<T>> TaskAsyncResponse<T, TR>(this Result<Task<TR>> result)
        {
            if (result == null) return Result<T>.Fail<T>("Data not found");
            if (!result.Success)
                return Result<T>.Fail<T>(result.Error);
            var items = await result.Value;
            return Result<T>.Ok(items.MapTo<T>());
        }
        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (!result.Failure) return result;
            action();
            return result;
        }

        public static Result<T> OnBoth<T>(this Result<T> result, Action<Result<T>> action)
        {
            action(result);
            return result;
        }

        public static T OnBoth<T>(this Result<T> result, Func<Result<T>, T> func) => func(result);
    }
}