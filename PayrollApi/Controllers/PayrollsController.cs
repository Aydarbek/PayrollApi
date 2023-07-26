using Microsoft.AspNetCore.Mvc;
using PayrollApi.Models.Dtos;
using PayrollApi.Services.Abstract;

namespace PayrollApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PayrollsController : ControllerBase
{
    private readonly IPayrollService _payrollService;


    public PayrollsController(IPayrollService payrollService)
    {
        _payrollService = payrollService;
    }

    [HttpGet]
    public async Task<List<PayrollReadDto>> GetAll()
    {
        return await _payrollService.GetAll();
    }

    [HttpGet("{id:long}")]
    public async Task<PayrollReadDto> Get(long id)
    {
        return await _payrollService.Get(id);
    }

    [HttpGet]
    public async Task GetStatistics()
    {
        var file = await _payrollService.GetCsvReport();
        HttpContext.Response.Headers.ContentDisposition = $"attachment; filename={file.Name}";
        await HttpContext.Response.SendFileAsync(file);
    }

    [HttpPost]
    public async Task Add([FromBody] PayrollWriteDto model)
    {
        await _payrollService.Create(model);
    }

    [HttpPut("{id:long}")]
    public async Task Update(long id, [FromBody] PayrollWriteDto model)
    {
        await _payrollService.Update(id, model);
    }

    [HttpDelete("{id:long}")]
    public async Task Delete(long id)
    {
        await _payrollService.Delete(id);
    }
}