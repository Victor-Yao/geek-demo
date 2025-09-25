using System;
using Microsoft.Extensions.Configuration;

namespace EnvironmentVariablesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run EnvironmentVariablesDemo");

            var builder = new ConfigurationBuilder();
            // builder.AddEnvironmentVariables();

            // var configurationRoot = builder.Build();
            // Console.WriteLine($"KEY1: {configurationRoot["KEY1"]}");

            #region section key
            // // "SECTION1__KEY3": "value3"
            // var section = configurationRoot.GetSection("SECTION1");
            // Console.WriteLine($"KEY3: {section["KEY3"]}");

            // // "SECTION1__SECTION2__KEY4": "value4"
            // var section2 = section.GetSection("SECTION2");
            // Console.WriteLine($"KEY4: {section2["KEY4"]}");
            #endregion

            #region prefix filer
            // "YAO_KEY1": "yao key1"
            builder.AddEnvironmentVariables("YAO_");
            var configurationRoot = builder.Build();
            Console.WriteLine($"KEY1: {configurationRoot["KEY1"]}");
            Console.WriteLine($"KEY2: {configurationRoot["KEY2"]}");
            #endregion
        }
    }
}
