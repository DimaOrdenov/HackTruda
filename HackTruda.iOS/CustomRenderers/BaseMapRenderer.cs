using System;
using System.Collections.Generic;
using CoreGraphics;
using HackTruda.iOS.CustomRenderers;
using HackTruda.ViewControls;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseMap), typeof(BaseMapRenderer))]
namespace HackTruda.iOS.CustomRenderers
{
    public class BaseMapRenderer : MapRenderer
    {
        private IEnumerable<BaseMapPin> _customPins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            MKMapView nativeMap = Control as MKMapView;

            if (e.OldElement != null &&
                nativeMap != null)
            {
                nativeMap.RemoveAnnotations(nativeMap.Annotations);
                nativeMap.GetViewForAnnotation = null;
            }

            if (e.NewElement is BaseMap formsMap &&
                nativeMap != null)
            {
                _customPins = formsMap.CustomPins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
            }
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is MKUserLocation ||
                !(annotation is MKPointAnnotation mKPointAnnotation) ||
                !(GetCustomPin(mKPointAnnotation) is BaseMapPin pin))
            {
                return null;
            }

            MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(pin.Label);

            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, pin.Label);
                annotationView.Image = UIImage.FromFile("map_pin_blue.png");
                annotationView.CalloutOffset = new CGPoint(0, 0);
            }

            annotationView.CanShowCallout = true;

            return annotationView;
        }

        private BaseMapPin GetCustomPin(MKPointAnnotation annotation)
        {
            if (_customPins == null)
            {
                return null;
            }

            Position position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);

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
