using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Responses;
using HackTruda.Services.Interfaces;
using RestSharp;

namespace HackTruda.BusinessLogic
{
    /// <summary>
    /// BL для игр.
    /// </summary>
    public class GamesLogic : BaseLogic<GamesResponse>, IGamesLogic
    {
        public GamesLogic(IRestClient client, UserContext context, IDebuggerService debuggerService)
            : base(client, context, debuggerService)
        {
        }

        protected override string Route => "games";
    }
}
