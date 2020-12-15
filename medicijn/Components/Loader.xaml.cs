using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace medicijn.Components
{
    public partial class Loader : ContentView
    {

        private CancellationTokenSource cancellationTokenSource;

        public static readonly BindableProperty IsSpinningProperty
             = BindableProperty.Create(
        nameof(IsSpinning),
        typeof(bool),
        typeof(Loader));
        

        public bool IsSpinning
        {
            get => (bool)GetValue(IsSpinningProperty);
            set
            {
                SetValue(IsSpinningProperty, value);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName == IsSpinningProperty.PropertyName) 
            {
                if (IsSpinning) 
                { 
                    cancellationTokenSource = new CancellationTokenSource();
                    Rotate(cancellationTokenSource.Token);
                }
                else
                {
                    cancellationTokenSource?.Cancel();
                }
            }
        }

        public Loader()
        {
            InitializeComponent();
        }

        private async void Rotate(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            { 
                await Container.RotateTo(360, 1000);
                await Container.RotateTo(0, 0);
            }
        }
    }
}
