using System;
using System.Threading.Tasks;

namespace DeFuncto.Extensions
{
    public static class Tests
    {
        public static Result<T, Exception> Try<T>(Func<T> func)
        {
            try
            {
                return func().Apply(Result<T, Exception>.Ok);
            }
            catch (Exception ex)
            {
                return Result<T, Exception>.Error(ex);
            }
        }

        public static AsyncResult<T, Exception> Try<T>(Func<Task<T>> func)
        {
            return Go();
            async Task<Result<T, Exception>> Go()
            {
                try
                {
                    return await func().Map(Result<T, Exception>.Ok);
                }
                catch (Exception ex)
                {
                    return Result<T, Exception>.Error(ex);
                }
            }
        }
    }
}
