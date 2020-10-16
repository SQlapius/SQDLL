using System;
using System.Collections.ObjectModel;
using GZIDAL002.Patienten.Models;

namespace GZIDAL002.Recepten.Models
{
    public class Recept
    {
        public Patient Patient { get; set; }

        public string Medewerker { get; set; }

        public ObservableCollection<ReceptRegel> ReceptRegels { get; set; } = new ObservableCollection<ReceptRegel>();

        public void AddRegel()
        {
            ReceptRegels.Add(new ReceptRegel());
        }
    }
}
