namespace PayrollApi.Models.Dtos;

public class PayrollWriteDto
{
    public long? EmployeeId { get; set; }
    public DateTime? Date { get; set; }
    public float? Amount { get; set; }
    public Status? Status { get; set; }
}