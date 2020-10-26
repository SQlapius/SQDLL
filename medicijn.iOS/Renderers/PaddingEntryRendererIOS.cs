using System;
using Xamarin.Forms;
using medicijn.Renderers;
using medicijn.iOS.Renderers;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer(typeof(PaddingEntryRenderer), typeof(PaddingEntryRendererIOS))]
namespace medicijn.iOS.Renderers
{
    public class PaddingEntryRendererIOS : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.RightView = new UIView(new CGRect(0, 0, 25, 0));
                Control.RightViewMode = UITextFieldViewMode.Always;
            }
        }
    }
}
