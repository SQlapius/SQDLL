using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Patienten.Models;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class CI
    {
        [JsonProperty("medId")]
        public int MedId { get; set; }

        [JsonProperty("cicode")]
        public int Cicode { get; set; }

        [JsonProperty("ciaard_nr")]
        public int CiaardNr { get; set; }

        [JsonProperty("op_Scherm")]
        public int OpScherm { get; set; }

        [JsonProperty("op_lijst")]
        public int OpLijst { get; set; }

        [JsonProperty("bs")]
        public int Bs { get; set; }

        [JsonProperty("b")]
        public string B { get; set; }

        [JsonProperty("ciaard")]
        public string Ciaard { get; set; }
    }

    public class IA
    {
        [JsonProperty("idA")]
        public int IdA { get; set; }

        [JsonProperty("dB")]
        public int DB { get; set; }

        [JsonProperty("Naam_a")]
        public string NaamA { get; set; }

        [JsonProperty("naam_b")]
        public string NaamB { get; set; }

        [JsonProperty("w")]
        public string W { get; set; }

        [JsonProperty("ia")]
        public string Ia { get; set; }

        [JsonProperty("av")]
        public string Av { get; set; }

        [JsonProperty("prio")]
        public int Prio { get; set; }

        [JsonProperty("iakode")]
        public int Iakode { get; set; }
    }

    internal class Root
    {
        [JsonProperty("receptregel")]
        public List<Recept> Recept { get; set; }
    }

    public class Recept
    {
        [JsonIgnore]
        public Patient Patient { get; set; }

        [JsonIgnore]
        public ObservableCollection<ReceptRegel> ReceptRegels { get; set; } = new ObservableCollection<ReceptRegel>();

        public string Medewerker { get; set; }

        [JsonProperty("recId")]
        public int RecId { get; set; }
   
        [JsonProperty("CI")]
        public List<CI> CI { get; set; }

        [JsonProperty("IA")]
        public List<IA> IA { get; set; }

        /*
         *  Creates Recept
         *  Recept always has to be initalized with a medicine
         */
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

        public void AddRegel(Medicijn medicijn, int aantal, string dosering)
        {
            ReceptRegels.Add(new ReceptRegel()
            {
                Medicijn = medicijn,
                Aantal = aantal,
                Dosering = dosering
            });
        }
    }
}
