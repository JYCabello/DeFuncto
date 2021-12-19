﻿using System;

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
    }
}
