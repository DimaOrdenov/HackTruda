using System;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Responses;
using HackTruda.Services.Interfaces;
using RestSharp;

namespace HackTruda.BusinessLogic
{
    /// <summary>
    /// BL для пользователей.
    /// </summary>
    public class UsersLogic : BaseLogic<UserResponse>, IUsersLogic
    {
        public UsersLogic(IRestClient client, UserContext context, IDebuggerService debuggerService)
            : base(client, context, debuggerService)
        {
        }

        protected override string Route => "users";
    }
}
