using System;
using System.Diagnostics;
using System.IO;
using GZIDAL001;
using GZIDAL001.Patient;
using Newtonsoft.Json;

namespace ConsoleTest
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine(JsonConvert.SerializeObject(await PatientService.ZoekPatientAsync("1999", 119)));
        }
    }
}
