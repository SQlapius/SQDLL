using System;
using GZIDAL002.Medicijnen.Models;

namespace GZIDAL002.Recepten.Models
{
    public class ReceptRegel
    {
        public Medicijn Medicijn { get; set; }

        public int Aantal { get; set; }

        public string Dosering { get; set; }
    }
}
