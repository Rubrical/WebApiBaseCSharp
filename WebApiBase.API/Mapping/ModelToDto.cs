using AutoMapper;
using WebApiBase.Data.DTOs;
using WebApiBase.Models;

namespace WebApiBase.Mapping;

public class ModelToDto : Profile
{
    public ModelToDto()
    {
        CreateMap<EmployeeModel, EmployeeDTO>();
    }
}