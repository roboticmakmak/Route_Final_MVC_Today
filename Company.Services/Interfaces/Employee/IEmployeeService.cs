using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data.Entites;
using Company.Services.Interfaces.Employee.Dto;

namespace Company.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto GetById(int? id);
        IEnumerable<EmployeeDto> GetAll();
        void Add(EmployeeDto employee);
         void Update(EmployeeDto employee);
         void Delete(EmployeeDto employee);
        public IEnumerable<EmployeeDto> GetEmployeesByName(string name);
    }
}
