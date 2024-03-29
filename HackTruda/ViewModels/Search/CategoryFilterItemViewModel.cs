﻿namespace HackTruda.ViewModels.Search
{
    /// <summary>
    /// VM для фильтров категорий.
    /// </summary>
    public class CategoryFilterItemViewModel : BaseViewModel
    {
        private bool _isSelected;

        public CategoryFilterItemViewModel(string title)
        {
            Title = title;
        }

        public string Title { get; }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
