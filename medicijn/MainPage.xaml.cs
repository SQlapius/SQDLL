﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZIDAL002.Patient;
using Xamarin.Forms;

namespace medicijn
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            ok();
        }

        async public void ok()
        {
            var patientService = new PatientService();

            Debug.WriteLine(await patientService.ZoekPatient(0195, "1999"));
        }
    }
}
