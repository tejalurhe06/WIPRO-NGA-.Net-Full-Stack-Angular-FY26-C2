using Microsoft.EntityFrameworkCore;
using RepositoryPatternEFCore.Models;

namespace RepositoryPatternEFCore.Repository
{
    public class EmpRepository : IEmpRepository
    {
        private readonly EmpContext _context;

        public EmpRepository(EmpContext context)
        {
            _context = context;
        }

        public IEnumerable<Emp> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Emp GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public int AddEmployee(Emp employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                _context.Employees.Add(employeeEntity);
                _context.SaveChanges();
                result = employeeEntity.EmpId;

            }

            return result;
        }

        public int UpdateEmployee(Emp employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                _context.Entry(employeeEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = employeeEntity.EmpId;
            }

            return result;
        }

        public int DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return 0;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
