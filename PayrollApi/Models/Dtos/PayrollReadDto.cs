namespace PayrollApi.Models.Dtos;

public class PayrollReadDto
{
    public long EmployeeId { get; set; }
    public long DepartmentId { get; set; }
    public string Date { get; set; }
    public float Amount { get; set; }
    public string Status { get; set; }
}