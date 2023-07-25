using AutoMapper;
using PayrollApi.Models;
using PayrollApi.Models.Dtos;

namespace PayrollApi.Profiles;

public class PayrollProfile : Profile
{
    public PayrollProfile()
    {
        CreateMap<Payroll, PayrollReadDto>()
            .ForMember(p => p.EmployeeId, opt => opt.MapFrom(x => x.EmployeeId))
            .ForMember(p => p.DepartmentId, opt => opt.MapFrom(x => x.Employee.DepartmentId))
            .ForMember(p => p.Date, opt => opt.MapFrom(x => x.Date.Value.ToString("yyyy-MM-dd")))
            .ForMember(p => p.Amount, opt => opt.MapFrom(x => x.Amount))
            .ForMember(p => p.Status, opt => opt.MapFrom(x => x.Status.ToString()));
        
        CreateMap<PayrollWriteDto, Payroll>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember, dstMember)
                => srcMember != null && dstMember != null));
    }
}