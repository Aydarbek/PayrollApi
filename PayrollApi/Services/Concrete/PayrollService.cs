using AutoMapper;
using PayrollApi.Models;
using PayrollApi.Models.Dtos;
using PayrollApi.Repository.Abstract;
using PayrollApi.Services.Abstract;

namespace PayrollApi.Services.Concrete;

public class PayrollService : IPayrollService
{
    private readonly IRepository<Payroll> _repo;
    private readonly IMapper _mapper;

    public PayrollService(IRepository<Payroll> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<PayrollReadDto> Get(long id, CancellationToken token = default)
    {
        var payroll = await _repo.Get(id, token);
        return _mapper.Map<PayrollReadDto>(payroll);
    }

    public async Task<List<PayrollReadDto>> GetAll(CancellationToken token = default)
    {
        var payrolls = await _repo.GetAll(token);
        return _mapper.Map<List<PayrollReadDto>>(payrolls);
    }

    public async Task Create(PayrollWriteDto model, CancellationToken token = default)
    {
        var payroll = _mapper.Map<Payroll>(model);
        await _repo.Add(payroll, token);
        await _repo.Save(token);
    }

    public async Task Update(long id, PayrollWriteDto model, CancellationToken token = default)
    {
        var payroll = await _repo.Get(id, token);
        _mapper.Map(model, payroll);

        var updPayroll = _mapper.Map<Payroll>(model);
        _repo.Update(payroll);
        await _repo.Save(token);
    }

    public async Task Delete(long id, CancellationToken token = default)
    {
        var payroll = await _repo.Get(id, token);
        _repo.Delete(payroll);
        await _repo.Save(token);
    }
}