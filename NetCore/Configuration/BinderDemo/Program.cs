using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace BinderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run BinderDemo");
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");

            var root = builder.Build();

            var config = new Config()
            {
                Key1 = "config key1",
                Key5 = false
            };

            // root.Bind(config);

            /// bind with section config 
            //root.GetSection("OrderService").Bind(config);

            /// bind with private Property
            root.GetSection("OrderService").Bind(config,
                bindOptions =>
                {
                    bindOptions.BindNonPublicProperties = true;
                });
            
            Console.WriteLine($"Key1: {config.Key1}");
            Console.WriteLine($"Key5: {config.Key5}");
            Console.WriteLine($"Key6: {config.Key6}");
        }
    }

    class Config
    {
        public string Key1 { get; set; }
        public bool Key5 { get; set; }
        // public int Key6 { get; set; }
        public int Key6 { get; private set; } = 400;
    }
}
