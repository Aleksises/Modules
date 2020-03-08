using CustomIOC.Attributes;
using System;

namespace SampleProject.SampleClasses
{
    [Export]
    public class Logger
    {
        public void HealthCheckLogger()
        {
            Console.WriteLine($"It's alive {GetType().Name}");
        }
    }
}
