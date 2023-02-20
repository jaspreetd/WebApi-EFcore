using WebApi_EFCore.Data.ORMEntity;

namespace WebApi_EFCore
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeDbContext db;

        public EmployeeRepo(EmployeeDbContext db)
        {
            this.db = db;
        }

        public List<Employee> GetEmployees() => db?.Employee?.ToList();

        public Employee PutEmployee(Employee employee)
        {
            db.Employee.Update(employee);
            db.SaveChanges();
            return db.Employee.Where(x => x.EmpID == employee.EmpID).FirstOrDefault();
        }

        public List<Employee> AddEmployee(Employee employee)
        {
            db.Employee.Add(employee);
            db.SaveChanges();
            return db.Employee.ToList();
        }

        public Employee GetEmployeeById(int Id)
        {
            return db.Employee.Where(x => x.EmpID == Id).FirstOrDefault();
        }
    }
}
