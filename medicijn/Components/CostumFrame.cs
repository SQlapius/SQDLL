using System;
using Xamarin.Forms;

namespace medicijn.Components
{
    public class CostumFrame : Frame
    {
        public static BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(CostumFrame), 4.0f);
        public static BindableProperty ShadowOpacityProperty = BindableProperty.Create(nameof(ShadowOpacity), typeof(float), typeof(CostumFrame), 0.2f);

        public float ShadowOpacity
        {
            get
            {
                return (float)GetValue(ShadowOpacityProperty);
            }
            set
            {
                SetValue(ShadowOpacityProperty, value);
            }
        }
        public float Elevation
        {
            get
            {
                return (float)GetValue(ElevationProperty);
            }
            set
            {
                SetValue(ElevationProperty, value);
            }
        }

        public CostumFrame()
        {
        }
    }
}
