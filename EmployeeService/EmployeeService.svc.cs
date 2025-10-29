using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using EmployeeService.Services;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeServiceLogic _logic;

        public EmployeeService()
        {
            var connectionSettings = ConfigurationManager.ConnectionStrings["Employee"];
            if (connectionSettings == null)
                throw new ConfigurationErrorsException("Connection string 'Employee' not found.");
            var connectionString = connectionSettings.ConnectionString;

            IEmployeeRepository repository = new EmployeeRepository(connectionString);
            _logic = new EmployeeServiceLogic(repository);
        }


        public bool GetEmployeeById(int id)
        {
            try
            {
                var employee = _logic.GetEmployeeTree(id);

                if (employee == null)
                {
                    SetResponseStatus(HttpStatusCode.NotFound);
            return false;
        }

                SetResponseStatus(HttpStatusCode.OK);
                return true;
            }
            catch (Exception ex)
            {
                SetResponseStatus(HttpStatusCode.InternalServerError);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void EnableEmployee(int id, int enable)
        {
            try
            {
                _logic.SetEnable(id, enable == 1);
                SetResponseStatus(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                SetResponseStatus(HttpStatusCode.InternalServerError);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
            
        }

        private void SetResponseStatus(HttpStatusCode statusCode)
        {
            var context = WebOperationContext.Current;
            if (context != null)
            {
                context.OutgoingResponse.StatusCode = statusCode;
            }
        }
    }
}