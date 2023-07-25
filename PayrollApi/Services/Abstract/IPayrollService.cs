using PayrollApi.Models;
using PayrollApi.Models.Dtos;

namespace PayrollApi.Services.Abstract;

public interface IPayrollService
{
    Task<PayrollReadDto> Get(long id, CancellationToken token = default);
    Task<List<PayrollReadDto>> GetAll(CancellationToken token = default);
    Task Create(PayrollWriteDto model, CancellationToken token = default);
    Task Update(long id, PayrollWriteDto model, CancellationToken token = default);
    Task Delete(long id, CancellationToken token = default);
}