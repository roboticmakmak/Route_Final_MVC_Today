using Company.Data.Contexts;
using Company.Data.Entites;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContexts _context;

        public EmployeeRepository(CompanyDbContexts context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeeByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        {
            name = name.ToLower().Trim();
            return _context.Employees
                .Where(x => x.Name.ToLower().Trim().Contains(name) ||
                            x.Email.ToLower().Trim().Contains(name) ||
                            x.PhoneNumer.ToLower().Trim().Contains(name))
                .ToList();
        }

    }
}
