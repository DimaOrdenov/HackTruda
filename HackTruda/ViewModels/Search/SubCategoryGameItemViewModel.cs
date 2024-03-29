﻿using HackTruda.DataModels.Responses;
using HackTruda.Definitions.Enums;

namespace HackTruda.ViewModels.Search
{
    /// <summary>
    /// VM для айтема подкатегории игры.
    /// </summary>
    public class SubCategoryGameItemViewModel : SubCategoryItemViewModel
    {
        public SubCategoryGameItemViewModel(GamesResponse game)
            : base(CategoryType.Games, CategoryType.Games, game.Name, game.Genre)
        {
            GameItem = game;
        }

        public GamesResponse GameItem { get; }
    }
}
