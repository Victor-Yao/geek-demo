using System;

namespace ScopeAndDisposableDemo.Services
{
    public interface IOrderService { }

    public class DisposableOrderService : IOrderService, IDisposable
    {
        public DisposableOrderService()
        {
            // Console.WriteLine($"DisposableOrderService {this.GetHashCode()}");
        }
        
        public void Dispose()
        {
            Console.WriteLine($"DisposableOrderService Disposed:{this.GetHashCode()}");
        }
    }
}