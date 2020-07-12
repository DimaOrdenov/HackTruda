using HackTruda.DataModels.Requests;
using HackTruda.DataModels.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace HackTruda.BusinessLogic.Interfaces
{
    public interface IAuthLogic
    {
        Task<AuthResponse> Register(RegisterRequest request, CancellationToken token);

        Task<string> Check(CancellationToken token);
    }
}
