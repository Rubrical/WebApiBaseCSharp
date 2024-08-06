using AutoMapper;
using WebApiBase.Data.DTOs;
using WebApiBase.Models;

namespace WebApiBase.Mapping;

public class DtoToModel : Profile
{
    public DtoToModel()
    {
        CreateMap<EmployeeDTO, EmployeeModel>();
        CreateMap<EditedEmployeeDto, EmployeeModel>();
    }
}