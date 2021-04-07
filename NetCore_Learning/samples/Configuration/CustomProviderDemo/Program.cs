using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace CustomProviderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run CustomProviderDemo!");
            var builder = new ConfigurationBuilder();
            builder.AddMyConfiguration();

            var root = builder.Build();
            int count = 0;

            ChangeToken.OnChange(
                () => root.GetReloadToken(),
                () =>
                {
                    Console.WriteLine($"========={count++}=========");
                    Console.WriteLine($"lastTime:{root["lastTime"]}");
                });

            Console.WriteLine("start!");
            Console.Read();
        }
    }
}
