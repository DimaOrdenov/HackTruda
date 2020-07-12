using System;
using System.Collections.ObjectModel;
using System.Linq;
using HackTruda.Extensions;

namespace HackTruda.ViewModels.Search
{
    /// <summary>
    /// VM для группы результата поиска.
    /// </summary>
    public class SubCategorySearchGroupViewModel : ObservableCollection<SubCategoryItemViewModel>
    {
        public string Category => this.FirstOrDefault()?.ParenType.GetEnumDescription();
    }
}
