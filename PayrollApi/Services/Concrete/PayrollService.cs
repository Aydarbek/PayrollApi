﻿using System.Globalization;
using System.Text;
using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using PayrollApi.Models;
using PayrollApi.Models.Dtos;
using PayrollApi.Repository.Abstract;
using PayrollApi.Services.Abstract;

namespace PayrollApi.Services.Concrete;

public class PayrollService : IPayrollService
{
    private readonly IRepository<Payroll> _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<PayrollService> _logger;
    private readonly ReportSettings _settings;

    public PayrollService(IRepository<Payroll> repo, IMapper mapper, IOptions<ReportSettings> settings, ILogger<PayrollService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _settings = settings.Value;
        _logger = logger;
    }
    
    public async Task<PayrollReadDto> Get(long id, CancellationToken token = default)
    {
        try
        {
            var payroll = await _repo.Get(id, token);
            return _mapper.Map<PayrollReadDto>(payroll);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during get payroll with id {}: {}", id, e.Message);
            return null;
        }
    }

    public async Task<List<PayrollReadDto>> GetAll(CancellationToken token = default)
    {
        try
        {
            var payrolls = await _repo.GetAll(token);
            return _mapper.Map<List<PayrollReadDto>>(payrolls);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during getting list of payrolls, {}", e.Message);
            return null;
        }
    }

    public async Task Create(PayrollWriteDto model, CancellationToken token = default)
    {
        try
        {
            var payroll = _mapper.Map<Payroll>(model);
            await _repo.Add(payroll, token);
            await _repo.Save(token);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during create payroll: {}", e.Message);
        }
    }

    public async Task Update(long id, PayrollWriteDto model, CancellationToken token = default)
    {
        try
        {
            var payroll = await _repo.Get(id, token);
            _mapper.Map(model, payroll);

            _mapper.Map<Payroll>(model);
            _repo.Update(payroll);
            await _repo.Save(token);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during update payroll with id {}:\n {}", id, e.Message);
        }
    }

    public async Task Delete(long id, CancellationToken token = default)
    {
        try
        {
            var payroll = await _repo.Get(id, token);
            _repo.Delete(payroll);
            await _repo.Save(token);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during delete payroll: {}", e.Message );
        }
    }

    public async Task<IFileInfo> GetCsvReport(CancellationToken token = default)
    {
        try
        {
            var payrolls = await GetAll(token);
            var filePath = GetFileName();
        
            await using var writer = new StreamWriter(filePath);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(payrolls, token);

            return GetFileInfo(filePath);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during report building: {}", e.Message);
            return null;
        }
    }

    private string GetFileName()
    {
        var sb = new StringBuilder();
        sb.Append(_settings.ReportsDirectory)
            .Append(_settings.ReportPrefix).Append('_')
            .Append(DateTime.Now.ToString(_settings.DateFormat))
            .Append(_settings.FileFormat);

        return sb.ToString();
    }

    private IFileInfo GetFileInfo(string filePath)
    {
        var fileProvider = new PhysicalFileProvider(Path.GetFullPath(_settings.ReportsDirectory));
        return fileProvider.GetFileInfo(Path.GetFileName(filePath));
    }
}