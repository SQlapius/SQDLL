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
            var test = new Patient();
            var lol = await test.FindPatientAsync("1999123", 119);
            Console.WriteLine(JsonConvert.SerializeObject(lol));
        }
    }
}
