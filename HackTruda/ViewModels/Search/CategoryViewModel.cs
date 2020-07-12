using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;

namespace HackTruda.ViewModels.Search
{
    /// <summary>
    /// VM для сраницы категории.
    /// </summary>
    public class CategoryViewModel : PageViewModel
    {
        public ICommand FilterTapCommand { get; }

        public CategoryViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            FilterTapCommand = BuildPageVmCommand<CategoryFilterItemViewModel>(
                (item) =>
                {
                    if (item.IsSelected)
                    {
                        return Task.CompletedTask;
                    }

                    Filters.First(x => x.IsSelected).IsSelected = false;

                    item.IsSelected = true;

                    return Task.CompletedTask;
                });

            Filters = new List<CategoryFilterItemViewModel>();
        }

        public CategoryItemViewModel CategoryItem { get; private set; }

        public IEnumerable<CategoryFilterItemViewModel> Filters { get; private set; }

        public override void Prepare(object parameter)
        {
            base.Prepare(parameter);

            if (parameter is CategoryItemViewModel categoryItem)
            {
                CategoryItem = categoryItem;

                List<CategoryFilterItemViewModel> filters = new List<CategoryFilterItemViewModel>
                {
                    new CategoryFilterItemViewModel("Все")
                    {
                        IsSelected = true,
                    },
                };

                filters.AddRange(
                    categoryItem
                        .SubCategoryItems
                        .Select(x => x.Type.GetEnumDescription())
                        .Distinct()
                        .Select(x => new CategoryFilterItemViewModel(x))
                        .ToList());

                Filters = filters;

                PageTitle = categoryItem.Title;
            }
        }
    }
}
