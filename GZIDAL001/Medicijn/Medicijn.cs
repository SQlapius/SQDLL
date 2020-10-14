using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace GZIDAL001.Medicijn
{
    public class Medicijn
    {
        private MedicijnService _medicijnService = new MedicijnService();

        public string NAAM { get; set; }
        public int PRKODE { get; set; }
        public int GPKODE { get; set; }
        public int ATKODE { get; set; }

        public Medicijn()
        {
        }

        public async Task<List<Medicijn>> ZoekMed(string parameter, int opPrk, int opNaamAtcMemo)
        {
            try
            {
                var data = await _medicijnService.GetMed(parameter, opPrk, opNaamAtcMemo);

                var medicijnen = (
                    from DataRow dr in data.Tables[0].Rows
                    select new Medicijn()
                    {
                        NAAM = dr["NAAM"].ToString(),
                    }
                ).ToList();

                return medicijnen;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }
    }
}
