using Microsoft.EntityFrameworkCore;

namespace EmployeeCostPreview.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Dependent> Dependents => Set<Dependent>();
    }
}
