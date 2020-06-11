using CustomIOC.Attributes;
using System;

namespace SampleProject.SampleClasses
{
    public interface ICustomerDAL
    {
        void HealthCheckDAL();
    }

    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {
        public void HealthCheckDAL()
        {
            Console.WriteLine($"It's alive {GetType().Name}");
        }
    }
}
