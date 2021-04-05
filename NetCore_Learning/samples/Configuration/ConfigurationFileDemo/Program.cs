using System;
using Microsoft.Extensions.Configuration;

namespace ConfigurationFileDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run ConfigurationFileDemo!");

            var builder = new ConfigurationBuilder();

            /// optional: true, allow file is inexistance
            /// reloadOnChange: true, will reloaded file if it changes.
            // builder.AddJsonFile("appsettings_abc.json", optional: true, reloadOnChange: true);
            builder.AddIniFile("appsettings.ini");
            builder.AddJsonFile("appsettings.json",optional:false,reloadOnChange:true);
            var root = builder.Build();

            Console.WriteLine($"Key1: {root["key1"]}");
            Console.WriteLine($"Key2: {root["key2"]}");
            Console.WriteLine($"Key3: {root["key3"]}");
            Console.ReadKey();

            Console.WriteLine($"Key1: {root["key1"]}");
            Console.WriteLine($"Key2: {root["key2"]}");
            Console.WriteLine($"Key3: {root["key3"]}");
            Console.ReadKey();
        }
    }
}
