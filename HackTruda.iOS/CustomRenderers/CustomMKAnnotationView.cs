using MapKit;

namespace HackTruda.iOS.CustomRenderers
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
            : base(annotation, id)
        {
        }
    }
}
