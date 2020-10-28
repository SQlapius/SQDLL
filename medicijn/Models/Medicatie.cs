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
            base.MedId = med.MedId;
            base.Naam = med.Naam;
            base.LastRecDt = med.LastRecDt;
            base.Passport = med.Passport;
            base.PrKode = med.PrKode;
            base.Telrec = med.Telrec;
            base.VasteMedicatie = med.VasteMedicatie;
            base.Dosering = med.Dosering;
        }
    }
}
