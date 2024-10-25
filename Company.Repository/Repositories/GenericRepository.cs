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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContexts _context;

        public GenericRepository(CompanyDbContexts contexts)
        {
                _context = contexts; 
        }
        void IGenericRepository<T>.Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        void IGenericRepository<T>.Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        IEnumerable<T> IGenericRepository<T>.GetAll()
            => _context.Set<T>().ToList();

        T IGenericRepository<T>.GetbyId(int id) => _context.Set<T>().Find(id);

        void IGenericRepository<T>.Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
