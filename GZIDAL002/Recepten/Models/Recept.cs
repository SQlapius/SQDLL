using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Patienten.Models;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class Recept
    {
        public int Id { get; set; } = 0;
        public int RecId { get; set; } = 0;

        [JsonIgnore]
        public Patient Patient { get; set; }

        public ObservableCollection<ReceptRegel> ReceptRegels { get; set; } = new ObservableCollection<ReceptRegel>();

        [JsonIgnore]
        public string Medewerker { get; set; }

        public Recept(Patient pat, string medewerker)
        {
            Patient = pat;
            Medewerker = medewerker;
        }

        public Recept(
            Patient patient,
            Medicijn medicijn,
            string medewerker,
            int aantal,
            string dosering
        )
        {
            Patient = patient;
            Medewerker = medewerker;
            ReceptRegels = new ObservableCollection<ReceptRegel>()
            {
                new ReceptRegel
                {
                    Medicijn = medicijn,
                    Aantal = aantal,
                    Dosering = dosering
                }
            };
        }

        internal void AddRegel(ReceptRegel regel)
        {
            ReceptRegels.Add(regel);
        }
    }
}
