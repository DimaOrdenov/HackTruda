using System;
using System.IO;
using HackTruda.DataModels.Responses;
using HackTruda.Definitions.Enums;
using Xamarin.Forms;

namespace HackTruda.ViewModels.Search
{
    public class SubCategoryPeopleItemViewModel : SubCategoryItemViewModel
    {
        public SubCategoryPeopleItemViewModel(CategoryType type, CategoryType parenType, string title, string description)
            : base(type, parenType, title, description)
        {
        }

        public SubCategoryPeopleItemViewModel(UserResponse user)
            : base(
                  CategoryType.PeopleCountry,
                  CategoryType.PeopleCountry,
                  $"{user.FirstName} {user.LastName}",
                  $"{user.City}, из {user.Country}")
        {
            Image = user.Image != null ? ImageSource.FromStream(() => new MemoryStream(user.Image)) : null;
        }
    }
}
