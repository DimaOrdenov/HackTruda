using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HackTruda.DataModels.Responses;

namespace HackTruda.BusinessLogic.Interfaces
{
    public interface IPostsLogic : IBaseLogic<PostResponse>
    {
        
        public Task<IEnumerable<FeedResponse>> GetFeed(int id, int page, CancellationToken token);
    }
}
