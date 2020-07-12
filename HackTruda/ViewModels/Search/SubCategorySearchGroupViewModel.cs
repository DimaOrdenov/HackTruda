using System;
using System.Collections.ObjectModel;
using System.Linq;
using HackTruda.Extensions;

namespace HackTruda.ViewModels.Search
{
    public class SubCategorySearchGroupViewModel : ObservableCollection<SubCategoryItemViewModel>
    {
        public string Category => this.FirstOrDefault()?.ParenType.GetEnumDescription();
    }
}
