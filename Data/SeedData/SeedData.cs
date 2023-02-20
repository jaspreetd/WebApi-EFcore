using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Polly;

namespace WebApi_EFCore.Data.SeedData
{
    public class SeedData
    {
        public static void Initialize(EmployeeDbContext db)
        {
            //if (!db.Blogs.Any())
            //{
            //    db.Blogs.Add(new Blog()
            //    {
            //        Url = "http://www.google.com",
            //    });
            //    db.SaveChanges();
            //}

            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() =>
                {
                    db.Database.Migrate();
                    db.Database.EnsureCreated();                     
                    if (!db.Employee.Any())
                    {
                        db.Employee.Add(new ORMEntity.Employee()
                        {
                            EmpID=1,
                            EmpName="Jaspreet",
                            EmpDept="BI Architecture",
                            EmpCity="Hartford"
                        });
                        db.SaveChanges();
                    }
                });
        }
    }
}
