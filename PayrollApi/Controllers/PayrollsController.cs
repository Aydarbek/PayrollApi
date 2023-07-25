using Microsoft.AspNetCore.Mvc;
using PayrollApi.Models;
using PayrollApi.Models.Dtos;
using PayrollApi.Repository.Abstract;
using PayrollApi.Services.Abstract;

namespace PayrollApi.Controllers;

public class PayrollsController : Controller
{
    private readonly IRepository<Payroll> _payrolls;
    private readonly IPayrollService _payrollService;


    public PayrollsController(IRepository<Payroll> payrolls, IPayrollService payrollService)
    {
        _payrolls = payrolls;
        this._payrollService = payrollService;
    }

    [HttpGet]
    public async Task<List<PayrollReadDto>> GetAll()
    {
        return await _payrollService.GetAll();
    }

    [HttpGet]
    public async Task<PayrollReadDto> Get(long id)
    {
        return await _payrollService.Get(id);
    }

    [HttpPost]
    public async Task Add([FromBody] PayrollWriteDto model)
    {
        await _payrollService.Create(model);
    }

    [HttpPut]
    public async Task Update(long id, [FromBody] PayrollWriteDto model)
    {
        await _payrollService.Update(id, model);
    }

    [HttpDelete]
    public async Task Delete(long id)
    {
        await _payrollService.Delete(id);
    }
}