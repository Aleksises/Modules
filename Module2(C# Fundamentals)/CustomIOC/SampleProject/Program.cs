using CustomIOC;
using SampleProject.SampleClasses;
using System.Reflection;

namespace SampleProject
{
    public class Program
    {
        /// <summary>
        /// The First Example
        /// </summary>
        public static void Main()
        {
            var container = new CustomIoC();
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL>();

            customerBLL.CheckDAL();
            customerBLL.CheckLogger();

            customerBLL2.CheckLogger();
            customerBLL2.CheckDAL();
        }

        /// <summary>
        /// The Second Example
        /// </summary>
        //public static void Main()
        //{
        //    var container = new CustomIoC();

        //    container.AddType(typeof(CustomerBLL));
        //    container.AddType(typeof(Logger));
        //    container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

        //    var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
        //    var customerBLL2 = container.CreateInstance<CustomerBLL>();

        //    customerBLL.CheckDAL();
        //    customerBLL.CheckLogger();

        //    customerBLL2.CheckLogger();
        //    customerBLL2.CheckDAL();
        //}
    }
}
