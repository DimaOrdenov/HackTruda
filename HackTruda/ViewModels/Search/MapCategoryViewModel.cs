using System;
using System.Linq;
using System.Threading.Tasks;
using HackTruda.Definitions.Enums;
using HackTruda.Extensions;
using HackTruda.Services.Interfaces;
using HackTruda.ViewControls;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace HackTruda.ViewModels.Search
{
    public class MapCategoryViewModel : CategoryViewModel
    {
        public MapCategoryViewModel(INavigationService navigationService, IDialogService dialogService, IDebuggerService debuggerService)
            : base(navigationService, dialogService, debuggerService)
        {
            PlacesMap = new BaseMap();
        }

        public BaseMap PlacesMap { get; }

        public override async Task OnAppearing()
        {
            if (PageDidAppear)
            {
                return;
            }

            State = PageStateType.MinorLoading;

            if ((await CrossPermissionsExtension.CheckAndRequestPermissionIfNeeded(
                    new Permissions.LocationWhenInUse(),
                    new Permissions.LocationAlways()))
                        .All(x => x.Value == PermissionStatus.Granted))
            {
                PlacesMap.IsShowingUser = true;
            }

            Position mapPosition = await TryMoveToUserLocation();

            foreach (SubCategoryItemViewModel subCategoryItem in CategoryItem.SubCategoryItems)
            {
                double randomLatMultiplier = new Random().Next(0, 10) < 5 ? -1 : 1;
                double randomLatOffset = new Random().Next(1, 9) * 0.01 * randomLatMultiplier;

                double randomLongMultiplier = new Random().Next(0, 10) < 5 ? -1 : 1;
                double randomLongOffset = new Random().Next(1, 9) * 0.01 * randomLongMultiplier;

                BaseMapPin pin = new BaseMapPin
                {
                    Address = subCategoryItem.Description,
                    Label = subCategoryItem.Title,
                    Position = new Position(
                        mapPosition.Latitude + randomLatOffset,
                        mapPosition.Longitude + randomLongOffset),
                };

                PlacesMap.CustomPins.Add(pin);
                PlacesMap.Pins.Add(pin);
            }

            State = PageStateType.Default;

            await base.OnAppearing();
        }

        private async Task<Position> TryMoveToUserLocation()
        {
            Location locationToMove = null;

            try
            {
                locationToMove = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best));
            }
            catch (Exception e)
            {
                DebuggerService.Log(e);
            }

            if (locationToMove == null)
            {
                try
                {
                    locationToMove = await Geolocation.GetLastKnownLocationAsync();
                }
                catch (Exception e)
                {
                    DebuggerService.Log(e);
                }
            }

            if (locationToMove == null)
            {
                locationToMove = new Location(55.751314, 37.627335);
            }

            PlacesMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(locationToMove.Latitude, locationToMove.Longitude),
                    Distance.FromKilometers(8)));

            return new Position(locationToMove.Latitude, locationToMove.Longitude);
        }
    }
}
