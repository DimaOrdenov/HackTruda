﻿using System;
using System.Collections.Generic;
using HackTruda.Definitions.Enums;
using Xamarin.Forms;

namespace HackTruda.Definitions.PageStacks
{
    /// <summary>
    /// Класс для связки таб-страница и VM с указанием типа страницы, дочерних страниц, их иконок и названий в баре.
    /// </summary>
    public class TabbedPageStack : BasePageStack
    {
        public TabbedPageStack(
            TabbedPageType tabPageKey,
            Type tabPageType,
            Type tabViewModelType,
            IList<PageType> childrenPageKeys,
            IDictionary<PageType, string> childrenPageTitles = null)
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

        public TabbedPageType TabPageKey { get; }

        public IList<PageType> ChildrenPageKeys { get; }

        public IDictionary<PageType, string> ChildrenPageTitles { get; }

        public IDictionary<PageType, FileImageSource> ChildrenPageIcons { get; set; }
    }
}
