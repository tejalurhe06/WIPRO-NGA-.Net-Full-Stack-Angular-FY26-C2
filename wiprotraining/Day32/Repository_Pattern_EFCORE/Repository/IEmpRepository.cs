using Microsoft.EntityFrameworkCore;
using RepositoryPatternEFCore.Models;

namespace RepositoryPatternEFCore.Repository
{
    public interface IEmpRepository : IDisposable
    {
        IEnumerable<Emp> GetAllEmployees();
        Emp GetEmployeeById(int id);
        int AddEmployee(Emp employeeEntity);
        int UpdateEmployee(Emp employeeEntity);
        int DeleteEmployee(int id);
    }
}
