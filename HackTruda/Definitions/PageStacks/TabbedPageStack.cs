using System;
using System.Collections.Generic;
using HackTruda.Definitions.Enums;
using Xamarin.Forms;

namespace HackTruda.Definitions.PageStacks
{
    public class TabbedPageStack : BasePageStack
    {
        public TabbedPageStack(
            TabbedPageType tabPageKey,
            Type tabPageType,
            Type tabViewModelType,
            IList<PageType> childrenPageKeys,
            IDictionary<PageType, string> childrenPageTitles)
            : base(tabPageType, tabViewModelType)
        {
            if (childrenPageKeys.Count != childrenPageTitles.Count)
            {
                throw new ArgumentException("Number of children page titles must be equal to number of children.");
            }

            TabPageKey = tabPageKey;
            ChildrenPageKeys = childrenPageKeys;
            ChildrenPageTitles = childrenPageTitles;
        }

        public TabbedPageType TabPageKey { get; set; }

        public IList<PageType> ChildrenPageKeys { get; set; }

        public IDictionary<PageType, string> ChildrenPageTitles { get; set; }

        public IDictionary<PageType, FileImageSource> ChildrenPageIcons { get; set; }
    }
}
