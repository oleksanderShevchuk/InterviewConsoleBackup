using System.Collections.Generic;
using System.Data;
using EmployeeService.Model;

namespace EmployeeService.Services
{
    public interface IEmployeeRepository
    {
        List<EmployeeDto> GetAll();
        void UpdateEnable(int id, bool enable);
    }
}
