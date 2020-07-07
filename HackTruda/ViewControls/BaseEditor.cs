using System;
using Xamarin.Forms;

namespace HackTruda.ViewControls
{
    public class BaseEditor : Editor
    {
        public static readonly BindableProperty MaxHeightRequestProperty = BindableProperty.Create(
            nameof(MaxHeightRequest),
            typeof(double),
            typeof(BaseEditor),
            propertyChanged: (sender, newValue, oldValue) => ((BaseEditor)sender).InvalidateMeasure());

        public double MaxHeightRequest
        {
            get => (double)GetValue(MaxHeightRequestProperty);
            set => SetValue(MaxHeightRequestProperty, value);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            SizeRequest sizeRequest = base.OnMeasure(widthConstraint, heightConstraint);

            if (MaxHeightRequest > 0)
            {
                return new SizeRequest(new Size(
                    sizeRequest.Request.Width,
                    Math.Min(MaxHeightRequest, sizeRequest.Request.Height)));
            }

            return sizeRequest;
        }
    }
}
