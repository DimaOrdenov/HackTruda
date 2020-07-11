using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using HackTruda.Droid.CustomRenderers;
using HackTruda.ViewControls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(BaseMap), typeof(CustomMapRenderer))]
namespace HackTruda.Droid.CustomRenderers
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        private IEnumerable<BaseMapPin> _customPins;

        public CustomMapRenderer(Context context)
            : base(context)
        {
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            if (!(Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) is Android.Views.LayoutInflater inflater) ||
                !(GetCustomPin(marker) is BaseMapPin pin))
            {
                return null;
            }

            Android.Views.View view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

            if (view?.FindViewById<TextView>(Resource.Id.InfoWindowTitle) is TextView infoTitle)
            {
                infoTitle.Text = marker.Title;
            }

            if (view?.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle) is TextView infoSubtitle)
            {
                infoSubtitle.Text = marker.Snippet;
            }

            return view;
        }

        public Android.Views.View GetInfoWindow(Marker marker) =>
            null;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement is BaseMap formsMap)
            {
                _customPins = formsMap.CustomPins;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.map_pin_blue));

            return marker;
        }

        private void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
        }

        private BaseMapPin GetCustomPin(Marker annotation)
        {
            if (_customPins == null)
            {
                return null;
            }

            Position position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);

            foreach (BaseMapPin pin in _customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }

            return null;
        }
    }
}
