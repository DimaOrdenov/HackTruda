using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HackTruda.BusinessLogic.Interfaces
{
    public interface IBaseLogic<T>
    {
        Task<IEnumerable<T>> Get(CancellationToken token);

        Task<T> Get(int id, CancellationToken token);

        Task<bool> Post<TRequest>(TRequest requestModel, CancellationToken token);

        Task<bool> Put<TRequest>(TRequest requestModel, int id, CancellationToken token);

        Task<bool> Delete(int id, CancellationToken token);
    }
}
