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
    public  class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        private readonly CompanyDbContexts _context;

        public DepartmentRepository(CompanyDbContexts context) : base(context)
        {
            _context = context;
        }

    }
}

