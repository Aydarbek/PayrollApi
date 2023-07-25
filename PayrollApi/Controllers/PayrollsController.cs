using Microsoft.AspNetCore.Mvc;
using PayrollApi.Models;
using PayrollApi.Repository.Abstract;

namespace PayrollApi.Controllers;

public class PayrollsController : Controller
{
    private readonly IRepository<Payroll> _payrolls;

    public PayrollsController(IRepository<Payroll> payrolls)
    {
        _payrolls = payrolls;
    }

    [HttpGet]
    public async Task<List<Payroll>> GetAll()
    {
        return await _payrolls.GetAll();
    }

    [HttpGet]
    public async Task<Payroll> Get(long id)
    {
        return await _payrolls.Get(id);
    }

    [HttpPost]
    public async Task Add([FromBody] Payroll payroll)
    {
        await _payrolls.Add(payroll);
        await _payrolls.Save();
    }

    [HttpPut]
    public async Task Update([FromBody] Payroll payroll)
    {
        _payrolls.Update(payroll);
        await _payrolls.Save();
    }
}