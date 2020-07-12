using System;
using HackTruda.Definitions.Enums;

namespace HackTruda.Definitions.PageStacks
{
    /// <summary>
    /// Класс для связки страница-VM с указанием типа страницы.
    /// </summary>
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
