using AutoMapper;
using TaskSystem.Domain.Models;
using TaskSystem.Domain.DTOs;

namespace TaskSystem.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.NameEmployee, member => member.MapFrom(orig => orig.name));
        }
    }
}