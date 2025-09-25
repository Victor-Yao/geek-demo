using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConfigFileHotUpdateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run ConfigFileHotUpdateDemo");

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var root = builder.Build();
            int count = 0;

            // IChangeToken token = root.GetReloadToken();
            ChangeToken.OnChange(
                () => root.GetReloadToken(),
                () =>
                {
                    Console.WriteLine($"=========={count++}===========");
                    Console.WriteLine($"Key1: {root["key1"]}");
                    Console.WriteLine($"Key2: {root["key2"]}");
                    Console.WriteLine($"Key3: {root["key3"]}");
                    // Thread.Sleep(Source.ReloadDelay);
                    // Load(reload: true);
                });

            Console.WriteLine("start");
            Console.Read();
        }
    }
}