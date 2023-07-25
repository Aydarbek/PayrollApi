using Microsoft.EntityFrameworkCore;
using PayrollApi.Models;
using PayrollApi.Repository.Abstract;
using PayrollApi.Repository.Contexts;

namespace PayrollApi.Repository.Concrete;

public class PayrollRepository : IRepository<Payroll>
{
    private readonly PayrollContext _context;

    public PayrollRepository(PayrollContext context)
    {
        _context = context;
    }
    
    public async Task<Payroll> Get(long id, CancellationToken token = default) 
        => await _context.Payrolls.AsNoTracking().Include(p => p.Employee)
            .FirstOrDefaultAsync(p => p.Id == id, token);

    public async Task<List<Payroll>> GetAll(CancellationToken token = default) 
        => await _context.Payrolls.AsNoTracking().Include(p => p.Employee)
            .ToListAsync(token);

    public async Task Add(Payroll entity, CancellationToken token = default) 
        => await _context.Payrolls.AddAsync(entity, token);

    public void Update(Payroll entity) 
        => _context.Entry(entity).State = EntityState.Modified;
    
    public void Delete(Payroll entity) 
        => _context.Payrolls.Remove(entity);

    public async Task Save(CancellationToken token = default) 
        => await _context.SaveChangesAsync(token);

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if(_context != null)
            await _context.DisposeAsync();
    }
}