using System.Data;
using EmployeeService.Model;

namespace EmployeeService.Services
{
    public interface IEmployeeServiceLogic
    {
        EmployeeDto GetEmployeeTree(int id);
        void SetEnable(int id, bool enable);
        DataTable GetQueryResult(string query);
    }
}
