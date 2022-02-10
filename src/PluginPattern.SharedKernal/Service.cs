using System;

namespace PluginPattern.SharedKernal
{
    public interface IService
    {
        void Process(Notification notification);
    }
    public class Service : IService
    {
        public void Process(Notification notification)
        {
            Console.WriteLine("Handled");
        }
    }
}
