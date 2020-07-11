using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Requests;
using HackTruda.DataModels.Responses;
using HackTruda.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackTruda.BusinessLogic
{
    public class AuthLogic : BaseLogic<AuthResponse>, IAuthLogic
    {
        public AuthLogic(IRestClient client, UserContext context, IDebuggerService debuggerService) 
            : base(client, context, debuggerService)
        {
        }

        protected override string Route => "Auth";


       
            public Task<string> Check( CancellationToken token) =>
             ExecuteAsync<string>(
             new RestRequest(Route + $"/check", Method.GET)
                   , token);

       public Task<AuthResponse> Register(RegisterRequest request, CancellationToken token) =>
             ExecuteAsync<AuthResponse>(
             new RestRequest(Route + $"/register", Method.POST)
                 .AddJsonBody(request)
                   , token);

       
    }
}
