using System;
using System.Collections.Generic;
using System.Text;
using HCAM.Common.Wrappers;

namespace HCAM.Common.Extensions
{
    public static class CommonExtensions
    {
        public static TR Using<T, TR> (this T disposable, Func<T, TR> f) where T : IDisposable
        {
            using (disposable) return f(disposable);
        }

        public static Result<TR> TryCatch<T, TR> (this T value, Func<T, TR> f)
        {
            try
            {
                return Result<TR>.Ok(f(value));
            }
            catch (Exception e)
            {
                return Result<TR>.Fail<TR>(e.Message);
            }
        }

        public static Result<TR> TryCatch<T, TR>(this Result<T> value, Func<Result<T>, Result<TR>> f)
        {
            try
            {
                return value.Failure ? Result<TR>.Fail<TR>(value.Error) : f(value);
            }
            catch (Exception e)
            {
                return Result<TR>.Fail<TR>(e.Message);
            }
        }
    }
}
