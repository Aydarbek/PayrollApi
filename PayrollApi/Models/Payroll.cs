namespace PayrollApi.Models;

public class Payroll
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public float Amount { get; set; }
    public Status Status { get; set; }
    public Employee Employee { get; set; }
}