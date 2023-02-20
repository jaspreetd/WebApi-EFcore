using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApi_EFCore.Data.ORMEntity;
using Polly;

namespace WebApi_EFCore
{
    public class EmployeeDbContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public EmployeeDbContext() 
        { 
        
        }
        //Constructor with DbContextOptions and the context class itself
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings

            if (!options.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString("WebApiDB_SqlServer");
                options.UseSqlServer(connectionString);

                //options.UseSqlite("Data Source=AppData\\apidb.db");
            }
            
        }

        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity => {
                entity.ToTable("Employees").HasKey("EmpID");
                entity.Property(e => e.EmpID).HasColumnName("EmpID").IsRequired().UseSequence();

                entity.Property(e => e.EmpName)
                    .HasMaxLength(30)
                    .IsRequired()
                    .IsUnicode(false);
                entity.Property(e => e.EmpCity)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.EmpDept)
                    .HasMaxLength(20)               
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Employee>().HasData(new Employee { EmpID=1, EmpName="Jaspreet",EmpCity="Hartford", EmpDept="BI Architecture"});
        }
        public void MigrateDB()
        {
            Policy
            .Handle<Exception>()
            .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
            .Execute(() => Database.Migrate());

        }
    } 
}
