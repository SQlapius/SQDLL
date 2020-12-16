using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using medicijn.Components;
using medicijn.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CostumFrame), typeof(CostumFrameRendererIOS))]
namespace medicijn.iOS.Renderers
{
    public class CostumFrameRendererIOS : FrameRenderer
    {
        public CostumFrameRendererIOS()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            UpdateShadow();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            UpdateShadow();

        }
        private void UpdateShadow()
        {
            var costumFrame = (CostumFrame)Element;
            Layer.ShadowColor = UIColor.DarkGray.CGColor;
            Layer.ShadowOpacity = costumFrame.ShadowOpacity;
            Layer.ShadowRadius = costumFrame.Elevation;
            Layer.ShadowOffset = new SizeF(3, 5);

        }
    }
}
