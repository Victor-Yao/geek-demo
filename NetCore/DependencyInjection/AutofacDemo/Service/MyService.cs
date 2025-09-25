using System;

namespace AutofacDemo.Services
{
    public interface IMyService
    {
        void ShowCode();
    }

    public class MyService : IMyService
    {
        public void ShowCode()
        {
            Console.WriteLine($"MyService.ShowCode:{GetHashCode()}");
        }
    }

    public class MyServiceV2 : IMyService
    {
        public MyNameService NameService { get; set; }

        public void ShowCode()
        {
            Console.WriteLine($"MyServiceV2.ShowCode:{GetHashCode()}, NameService is empty?: {NameService == null}");
        }
    }

    public class MyNameService
    {
        
    }
}