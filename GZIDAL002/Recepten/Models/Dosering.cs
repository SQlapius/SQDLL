using System;
using System.Linq;
using System.Collections.Generic;

namespace GZIDAL002.Recepten.Models
{
    public class Dosering
    {
        DoseringTabellen _doseringTabellen;

        public string Frequentie { get; }
        public string Tijdseenheid { get; } = "";
        public string Aantal { get; }
        public string Advieseenheid { get; } = "";
        public string[] Info { get; }
        public string ExtraText { get; }

        public string CodeAsString
        {
            get => ConvertCodeToString(); 
        }

        public Dosering(string code, DoseringTabellen dt)
        {
            _doseringTabellen = dt;
            
            if (code[0] == ';')
            {
                ExtraText = RemoveSubstring(code, ";");
                return;
            }

            var codearr = code.Split(';');
            var dosering = codearr[0];
            var firstChar = dosering[0];

            if (firstChar != ' ')
            {
                Frequentie = firstChar != '-' ? GetCharactersUntilLetter(dosering) : "-";
                dosering = RemoveSubstring(dosering, Frequentie.ToString());

                Tijdseenheid = GetCharactersUntilDigit(dosering);
                dosering = RemoveSubstring(dosering, Tijdseenheid);

                Aantal = dosering != "" ? dosering[0] != ' ' ? GetCharactersUntilLetter(dosering) : " " : "";
                dosering = dosering.TrimStart(Aantal.ToString().ToCharArray());

                Advieseenheid = GetCharactersUntilDigit(dosering);
                dosering = dosering.TrimStart(Advieseenheid.ToString().ToCharArray());

                ExtraText = codearr.Length > 1 ? codearr[1] : "";
            }

            Info = dosering != "" ? GetInfo(dosering) : null;
        }

        private static string RemoveSubstring(string inputString, string removeString)
        {
            return inputString.TrimStart(removeString.ToCharArray());
        }

        private static string GetCharactersUntilDigit(string dosering)
        {
            for(var i = 0; i < dosering.Length; i++)
            { 
                if(!char.IsLetter(dosering[i]))
                {
                    return dosering.Substring(0, i);
                }
            }

            return dosering;
        }

        private static string GetCharactersUntilLetter(string dosering)
        {
            for(var i = 0; i < dosering.Length; i++)
            { 
                if(char.IsLetter(dosering[i]) || dosering[i] == '-' || dosering[i] == ' ')
                {
                    return dosering.Substring(0, i);
                }
            }

            return dosering;
        }

        private static string[] GetInfo(string dosering)
        {
            return dosering.Trim().Split(' ');
        }

        private string ConvertCodeToString()
        {
            var tijdseenheid = _doseringTabellen.TijdsEenheden
                .Where(x => x.Kode == Tijdseenheid)
                .FirstOrDefault()?.Omschrijving ?? "";

            var gebruikseenheid = _doseringTabellen.GebruiksEenheden
                .Where(x => x.Kode == Advieseenheid)
                .FirstOrDefault()?.Omschrijving ?? "";

            string infostring = "";

            if (Info != null && Info.Length > 0)
            {
                foreach (string s in Info)
                    infostring += (_doseringTabellen.AanvullendeTeksten
                        .Where(x => x.Kode == s).FirstOrDefault()?
                        .Omschrijving ?? "") + "; ";
            }

            return $"{Frequentie}x {tijdseenheid} {Aantal} {gebruikseenheid} info: {infostring} extratext: {ExtraText}";
        }
    }
}
