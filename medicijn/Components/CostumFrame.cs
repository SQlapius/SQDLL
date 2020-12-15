using System;
using Xamarin.Forms;

namespace medicijn.Components
{
    public class CostumFrame : Frame
    {
        public static BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(CostumFrame), 4.0f);

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
