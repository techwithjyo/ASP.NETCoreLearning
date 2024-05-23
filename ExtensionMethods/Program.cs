using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new { Name = "Jyotirmoy", age = 26 };
            string jsonString = JsonConvert.SerializeObject(person);

            Console.WriteLine(jsonString);

            string newJsonString = JsonExtensions.SerializeAndIndent(person);

            Console.WriteLine(newJsonString);
            Console.ReadKey();

        }
    }
}
