using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Patienten.Models;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class Recept
    {
        public Patient Patient { get; set; }

        public string Medewerker { get; set; }

        public ObservableCollection<ReceptRegel> ReceptRegels { get; set; } = new ObservableCollection<ReceptRegel>();

        public Recept(Patient patient)
        {

        }

        public void AddRegel(Medicijn medicijn, int aantal, string dosering)
        {
            ReceptRegels.Add(new ReceptRegel()
            {
                Naam = medicijn.Naam,
                Aantal = aantal,
                Dosering = dosering
            });
        }
    }
}
