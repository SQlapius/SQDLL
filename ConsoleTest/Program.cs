using System;
using System.Diagnostics;
using System.IO;
using GZIDAL001;
using GZIDAL001.Medicijn;
using GZIDAL001.Patient;
using Newtonsoft.Json;

namespace ConsoleTest
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var test = new Medicijn();
            var lol = await test.ZoekMed("OMEPRAZOLENSYRSPENDSFALKAKIT2MG", 2, 0);
            Console.WriteLine(JsonConvert.SerializeObject(lol));
        }
    }
}
