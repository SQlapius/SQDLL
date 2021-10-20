using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.Components
{
    public partial class MultiSelectBoxGroup : ContentView
    {
        public static readonly BindableProperty OptionsProperty
    = BindableProperty.Create(
        nameof(Options),
        typeof(IEnumerable),
        typeof(IEnumerable),
        propertyChanged: OptionsChanged);

        public IEnumerable Options
        {
            get => (IEnumerable)GetValue(OptionsProperty);
            set
            {
                SetValue(OptionsProperty, value);
            }
        }

        private static void OptionsChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public static readonly BindableProperty SelectedIdsProperty
            = BindableProperty.Create(
                nameof(SelectedIds),
                typeof(ObservableCollection<int>),
                typeof(ObservableCollection<int>),
                defaultValue: new ObservableCollection<int>() { 3, 1 },
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: IdChanged);

        public ObservableCollection<int> SelectedIds
        {
            get => (ObservableCollection<int>)GetValue(SelectedIdsProperty);
            set
            {
                SetValue(SelectedIdsProperty, value);
            }
        }

        private static void IdChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (MultiSelectBoxGroup)bindable;

            foreach (var c in view.Container.Children)
            {
                var child = (Frame)c;
                child.BackgroundColor = Color.FromHex("#F8F8F8");
                ((Label)child.Content).TextColor = Color.FromHex("#B2B2B2");
            }

            foreach(var id in (IEnumerable)newValue)
            {
                var frame = (Frame)view.Container.Children[(int)id];

                frame.BackgroundColor = Color.FromRgba(1, 153, 153, 60);
                ((Label)frame.Content).TextColor = Color.FromRgb(1, 153, 153);
            }
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Niet de beste manier...

            var id = (int)((TappedEventArgs)e).Parameter;

            var newList = new ObservableCollection<int>();

            if(SelectedIds.Contains(id))
            {
                SelectedIds.Remove(id);
            }
            else
            { 
                SelectedIds.Add(id);
            }


            for(var i = 0; i < SelectedIds.Count; i++)
            {
                newList.Add(SelectedIds[i]);
            }

            SelectedIds = newList;

        }
        public MultiSelectBoxGroup()
        {
            InitializeComponent();
        }
    }
}
