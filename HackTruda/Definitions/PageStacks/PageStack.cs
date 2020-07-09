using System;
using HackTruda.Definitions.Enums;

namespace HackTruda.Definitions.PageStacks
{
    public class PageStack : BasePageStack
    {
        public PageStack(PageType pageKey, Type pageClassType, Type viewModelClassType)
            : base(pageClassType, viewModelClassType)
        {
            PageKey = pageKey;
        }

        public PageType PageKey { get; }
    }
}
