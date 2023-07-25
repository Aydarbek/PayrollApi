using Microsoft.EntityFrameworkCore;
using PayrollApi.Models;

namespace PayrollApi.Repository.Contexts;

public class PayrollContext : DbContext
{
    public PayrollContext()
    {
    }

    public PayrollContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>().ToTable("employees");
        builder.Entity<Employee>().Property(e => e.Id).HasColumnName("id");
        builder.Entity<Employee>().Property(e => e.Name).HasColumnName("name")
            .HasMaxLength(30).IsRequired();
        builder.Entity<Employee>().Property(e => e.DepartmentId).HasColumnName("department_id");
        builder.Entity<Employee>().HasIndex(e => e.Name).IsUnique();
        builder.Entity<Employee>().HasOne(e => e.Department).WithMany().HasForeignKey(e => e.DepartmentId);

        builder.Entity<Department>().ToTable("departments");
        builder.Entity<Department>().Property(d => d.Id).HasColumnName("id");
        builder.Entity<Department>().Property(d => d.Name).HasColumnName("name")
            .HasMaxLength(30).IsRequired();;
        builder.Entity<Department>().HasIndex(d => d.Name).IsUnique();

        builder.Entity<Payroll>().ToTable("payrolls");
        builder.Entity<Payroll>().Property(p => p.Id).HasColumnName("id");
        builder.Entity<Payroll>().Property(p => p.EmployeeId).HasColumnName("employee_id");
        builder.Entity<Payroll>().Property(p => p.Date).HasColumnName("date");
        builder.Entity<Payroll>().Property(p => p.Amount).HasColumnName("amount");
        builder.Entity<Payroll>().Property(p => p.Status).HasColumnName("status");

        builder.Entity<Payroll>().HasOne(p => p.Employee).WithMany().HasForeignKey(p => p.EmployeeId);

        builder.Entity<Department>().HasData(new { Id = 1L, Name = "Administration" }, new { Id = 2L, Name = "Engineering" }, new { Id = 3L, Name = "Finances" });

        builder.Entity<Employee>().HasData(
            new { Id = 1L, Name = "Bob", DepartmentId = 1L },
            new { Id = 2L, Name = "Kasun", DepartmentId = 2L },
            new { Id = 3L, Name = "Andrew", DepartmentId = 2L },
            new { Id = 4L, Name = "Molly", DepartmentId = 3L }
        );

        builder.Entity<Payroll>().HasData(
            new { Id = 1L, EmployeeId = 1L, Amount = 20000f, Date = DateTime.ParseExact("2023-05-08", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), Status = Status.Pending },
            new { Id = 2L, EmployeeId = 2L, Amount = 30000f, Date = DateTime.ParseExact("2023-05-15", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), Status = Status.Approved },
            new { Id = 3L, EmployeeId = 3L, Amount = 15000f, Date = DateTime.ParseExact("2023-05-13", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), Status = Status.Approved },
            new { Id = 4L, EmployeeId = 4L, Amount = 10000f, Date = DateTime.ParseExact("2023-05-20", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), Status = Status.Pending }
        );
    }
}