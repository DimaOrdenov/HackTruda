using System;
using System.Collections.Generic;
using System.Security.Claims;
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
            new RestRequest(Route + $"/feed/{id}", Method.GET)
                  .AddParameter("page", page)
                  ,token);
        public Task<IEnumerable<FeedResponse>> GetUserPosts(int id, CancellationToken token) =>
          ExecuteAsync<IEnumerable<FeedResponse>>(
          new RestRequest(Route + $"/user/{id}", Method.GET)
                , token);

    }
}
