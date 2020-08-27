using System.Collections.Generic;
using HCAM.Common.Wrappers;

namespace HCAM.Common.Extensions
{
    public static class MapperExtensions
    {
        public static T MapTo<T>(this object value)
        {
            return AutoMapper.Mapper.Map<T>(value);
        }

        public static IEnumerable<T> EnumerableTo<T>(this object value)
        {
            return AutoMapper.Mapper.Map<IEnumerable<T>>(value);
        }

        public static Result<T> MapResultTo<T, TR>(this Result<TR> result)
        {
            return result.Success ?
                Result<T>.Ok(AutoMapper.Mapper.Map<T>(result.Value))
                : Result<T>.Fail<T>(result.Error);
        }

        public static Result<IEnumerable<T>> MapResultsTo<T, TR>(this Result<IEnumerable<TR>> result)
        {
            return result.Success ?
                Result<IEnumerable<T>>.Ok(AutoMapper.Mapper.Map<IEnumerable<T>>(result.Value))
                : Result<IEnumerable<T>>.Fail<IEnumerable<T>>(result.Error);
        }
    }
}
