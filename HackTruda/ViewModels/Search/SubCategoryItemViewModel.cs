using HackTruda.Definitions.Enums;

namespace HackTruda.ViewModels.Search
{
    public class SubCategoryItemViewModel : CategoryItemViewModel
    {
        public SubCategoryItemViewModel(CategoryType type, CategoryType parenType, string title, string description)
            : base(type)
        {
            ParenType = parenType;
            Title = title;
            Description = description;
        }

        public CategoryType ParenType { get; }
    }
}
