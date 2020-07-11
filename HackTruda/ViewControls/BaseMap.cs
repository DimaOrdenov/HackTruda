using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace HackTruda.ViewControls
{
    public class BaseMap : Map
    {
        public BaseMap()
        {
            CustomPins = new List<BaseMapPin>();
        }

        public IList<BaseMapPin> CustomPins { get; }
    }
}
