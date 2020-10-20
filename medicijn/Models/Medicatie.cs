using System;
using Med = GZIDAL002.Patienten.Models.Medicatie;

namespace medicijn.Models
{
    public class Medicatie : Med
    {
        public bool IsChecked { get; set; }
    }
}
