using System.Collections.Generic;
using System.Data;
using System.Linq;
using EmployeeService.Model;

namespace EmployeeService.Services
{
    public class EmployeeServiceLogic : IEmployeeServiceLogic
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeServiceLogic(IEmployeeRepository repo) { 
            _repo = repo;
        }

        public EmployeeDto GetEmployeeTree(int id)
        {
            var all = _repo.GetAll();
            var lookup = all.ToDictionary(x => x.ID, x => x);

            foreach (var emp in all)
            {
                if (emp.ManagerID.HasValue && lookup.ContainsKey(emp.ManagerID.Value))
                {
                    lookup[emp.ManagerID.Value].Employees.Add(emp);
                }
            }

            return lookup.ContainsKey(id) ? lookup[id] : null;
        }

        public void SetEnable(int id, bool enable)
        {
            _repo.UpdateEnable(id, enable);
        }
    }
}
