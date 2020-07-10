using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.DataModels.Responses;
using HackTruda.Services.Interfaces;
using RestSharp;

namespace HackTruda.BusinessLogic
{
    public class PostsLogic : BaseLogic<PostResponse>, IPostsLogic
    {
        public PostsLogic(IRestClient client, UserContext context, IDebuggerService debuggerService)
            : base(client, context, debuggerService)
        {
        }

        protected override string Route => "posts";

        public Task<IEnumerable<FeedResponse>> GetFeed(int id, int page, CancellationToken token) =>
            ExecuteAsync<IEnumerable<FeedResponse>>(
            new RestRequest(Route, Method.GET)
                  .AddParameter("id", id)
                  .AddParameter("page", page)
                  ,token);
        
    }
}
