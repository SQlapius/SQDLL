using System;
using System.ComponentModel;
using Med = GZIDAL002.Patienten.Models.Medicatie;

namespace medicijn.Models
{
    public class Medicatie : Med 
    {

        public bool IsChecked { get; set; } = false;

        public Medicatie(Med med)
        {
            MedId = med.MedId;
            Naam = med.Naam;
            LastRecDt = med.LastRecDt;
            Passport = med.Passport;
            PrKode = med.PrKode;
            Telrec = med.Telrec;
            VasteMedicatie = med.VasteMedicatie;
            Dosering = med.Dosering;
        }
    }
}
