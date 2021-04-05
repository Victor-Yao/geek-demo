using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run ConfigurationDemo");
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>(){
                { "key1","value1"},
                { "key2","value2"},
                { "Section1:Key4","value4"},
                { "Section2:Key5","value5"},
                { "Section2:Section3:Key7","value7"}
            });

            IConfigurationRoot configurationRoot = builder.Build();

            Console.WriteLine("============1=============");
            Console.WriteLine($"key1: {configurationRoot["key1"]}");
            Console.WriteLine($"key2: {configurationRoot["key2"]}");

            Console.WriteLine("============2=============");
            IConfigurationSection section1 = configurationRoot.GetSection("Section1");
            Console.WriteLine($"key4: {section1["key4"]}");
            Console.WriteLine($"key5: {section1["key5"]}");

            Console.WriteLine("============3=============");
            IConfigurationSection section2 = configurationRoot.GetSection("Section2");
            Console.WriteLine($"key5: {section2["key5"]}");
            Console.WriteLine($"key7: {section2["key7"]}");

            Console.WriteLine("============4=============");
            IConfigurationSection section3 = section2.GetSection("Section3");
            Console.WriteLine($"key7: {section3["key7"]}");
        }
    }
}
