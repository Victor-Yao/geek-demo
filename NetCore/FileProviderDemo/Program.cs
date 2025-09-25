using System;
using Microsoft.Extensions.FileProviders;

namespace FileProviderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run:");
            ///1. Physical FileProvider
            IFileProvider phyProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            // CurrentDomain.BaseDirectory: "\bin\Debug\net5.0\"
            // CurrentDomain.FriendlyName: "FileProviderDemo"

            var contents = phyProvider.GetDirectoryContents("/");

            foreach (var item in contents)
            {
                if (!item.IsDirectory)
                {
                    Console.WriteLine(item.Name);
                }
            }

            ///2. Embedded FileProvider
            ///Summary:
            ///     Embdeded resources is built within assembly. It must to be marked in project's property
            ///     It's not a physical resouce.
            IFileProvider embProvider = new EmbeddedFileProvider(typeof(Program).Assembly);
            var html = embProvider.GetFileInfo("emb.html");
            if(html.Exists){
                Console.WriteLine($"Embedded: { html.Name} existed");
            }

            // IFileProvider membProvider = new ManifestEmbeddedFileProvider(typeof(Program).Assembly);
            // var html1 = membProvider.GetFileInfo("emb.html");
            // if(html1.Exists){
            //     Console.WriteLine($"ManifestEmbedded: { html1.Name} existed");
            // }

            ///3. Composiste FileProvider = Physical + Embeded
            IFileProvider provider3 = new CompositeFileProvider(phyProvider,embProvider);
            contents = provider3.GetDirectoryContents("/");

            foreach (var item in contents)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
