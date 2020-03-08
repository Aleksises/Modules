using CustomIOC.Attributes;

namespace SampleProject.SampleClasses
{
    [ImportConstructor]
    public class CustomerBLL
    {
        private readonly ICustomerDAL _customerDAL;
        private readonly Logger _logger;

        public CustomerBLL(ICustomerDAL customerDAL, Logger logger)
        {
            _customerDAL = customerDAL;
            _logger = logger;
        }

        public void CheckDAL()
        {
            _customerDAL.HealthCheckDAL();
        }

        public void CheckLogger()
        {
            _logger.HealthCheckLogger();
        }
    }
}
