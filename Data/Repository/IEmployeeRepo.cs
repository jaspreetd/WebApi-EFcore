
using WebApi_EFCore.Data.ORMEntity;

namespace WebApi_EFCore
{
    public interface IEmployeeRepo
    {
        List<Employee> AddEmployee(Employee employee);
        Employee GetEmployeeById(int Id);
        List<Employee> GetEmployees();
        Employee PutEmployee(Employee employee);
    }
}