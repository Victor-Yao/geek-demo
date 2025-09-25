using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CommandLineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run CommandLineDemo");
            var builder = new ConfigurationBuilder();

            /// CommandLineKey1: value1
            // builder.AddCommandLine(args);


            #region replace command key
            /// CommandLineKey1: k3
            var mapper = new Dictionary<string, string> { { "--k1", "CommandLineKey1" } };
            builder.AddCommandLine(args, mapper);
            #endregion

            var configurationRoot = builder.Build();

            Console.WriteLine($"CommandLineKey1: {configurationRoot["CommandLineKey1"]}");
            Console.WriteLine($"CommandLineKey2: {configurationRoot["CommandLineKey2"]}");
            Console.WriteLine($"CommandLineKey3: {configurationRoot["CommandLineKey3"]}");
            Console.ReadKey();
        }
    }
}
