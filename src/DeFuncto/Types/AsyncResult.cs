using System.Threading.Tasks;

namespace DeFuncto
{
    public readonly struct AsyncResult<TOk, TError>
    {
        private readonly Task<Result<TOk, TError>> Result;
    }
}
