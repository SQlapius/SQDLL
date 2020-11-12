using System.Diagnostics;
using System.Linq;
using GZIDAL002.Recepten.Models;
using Newtonsoft.Json;

namespace medicijn.Utils
{
    public class Dosering
    {
        public string Frequentie { get; }
        public string Tijdseenheid { get; } = "";
        public string Aantal { get; }
        public string Advieseenheid { get; } = "";
        public string[] Info { get; }
        public string ExtraText { get; }

        public Dosering(string code)
        {
            if (code[0] == ';')
            {
                ExtraText = RemoveSubstring(code, ";");
                return;
            }

            var codearr = code.Split(';');

            var dosering = codearr[0];

            var firstChar = dosering[0];

            if(firstChar == ' ') {
                Info = GetInfo(dosering);
                return;
            }

            Frequentie = firstChar != '-' ? GetCharactersUntilLetter(dosering) : "-";
            dosering = RemoveSubstring(dosering, Frequentie.ToString());

            Tijdseenheid = GetCharactersUntilDigit(dosering);
            dosering = RemoveSubstring(dosering, Tijdseenheid);

            Aantal = dosering != "" ? dosering[0] != ' ' ? GetCharactersUntilLetter(dosering) : " " : "";
            dosering = dosering.TrimStart(Aantal.ToString().ToCharArray());
            
            Advieseenheid = GetCharactersUntilDigit(dosering);
            dosering = dosering.TrimStart(Advieseenheid.ToString().ToCharArray());

            Info = dosering != "" ? dosering.Trim().Split(' ') : null;

            ExtraText = codearr.Length > 1 ? codearr[1] : "";
        }

        private string RemoveSubstring(string inputString, string removeString)
        {
            return inputString.TrimStart(removeString.ToCharArray());
        }

        private string GetCharactersUntilDigit(string dosering)
        {
            return new string(dosering.TakeWhile(char.IsLetter).ToArray());
        }

        private string GetCharactersUntilLetter(string dosering)
        {
            return new string(dosering.TakeWhile(c =>
                char.IsDigit(c) ||
                c == '-' ||
                c == '.'
            ).ToArray());
        }

        private string[] GetInfo(string dosering)
        {
            return dosering.Trim().Split(' ');
        }

        public string ConvertCodeToString(DoseringTabellen _doseringTabellen)
        {
            var tijdseenheid = Tijdseenheid != "" ? 
                _doseringTabellen.TijdsEenheden.Where(x => x.Kode == Tijdseenheid)
                .FirstOrDefault()?.Omschrijving ?? "" : "";

            var gebruikseenheid = Advieseenheid != "" ?
                _doseringTabellen.GebruiksEenheden.Where(x => x.Kode == Advieseenheid)
                .FirstOrDefault()?.Omschrijving ?? "" : "";

            string infostring = "";

            if(Info != null && Info.Length > 0)
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
