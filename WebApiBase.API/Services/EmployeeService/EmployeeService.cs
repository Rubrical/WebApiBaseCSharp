using AutoMapper;
using WebApiBase.Data;
using WebApiBase.Data.DTOs;
using WebApiBase.Enums;
using WebApiBase.Exceptions;
using WebApiBase.Infrastructure;
using WebApiBase.Models;

namespace WebApiBase.Services.EmployeeService;

public class EmployeeService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper) : IEmployeeService
{
    public async Task<ServiceResponse<IEnumerable<EmployeeModel>>> GetPaginateAsync(int pageNumber, int pageSize)
    {
        var employees = await unitOfWork.EmployeesRepository.GetPaginateAsync(pageNumber, pageSize);
        if (employees is null)
        {
            throw new WebApiBaseException("It was not possible to connect to database",
                StatusCodes.Status500InternalServerError);
        }

        var employeeModels = employees.ToList();
        if (employeeModels.Count == 0)
        {
            throw new WebApiBaseException("No registries found", StatusCodes.Status500InternalServerError);
        }

        var response = new ServiceResponse<IEnumerable<EmployeeModel>>
        {
            Data = employeeModels,
            Success = true
        };

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> CreateNewAsync(EmployeeModel newEmployee)
    {
        if (newEmployee.Name == string.Empty || newEmployee.LastName == string.Empty)
        {
            throw new WebApiBaseException("Empty name and/or last name", StatusCodes.Status400BadRequest);
        }

        if (!Enum.IsDefined(typeof(ShiftEnum), newEmployee.Shift) ||
            !Enum.IsDefined(typeof(DepartmentEnum), newEmployee.Department)
           )
        {
            throw new WebApiBaseException("Invalid shift or department", StatusCodes.Status400BadRequest);
        }

        var now = DateTime.Now.ToLocalTime();
        newEmployee.DateCreation = now;
        newEmployee.UpdateDate = now;

        context.Add(newEmployee);
        await context.SaveChangesAsync();

        var response = new ServiceResponse<EmployeeModel>()
        {
            Data = newEmployee
        };

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> GetByIdAsync(int id)
    {
        // var employee = await context.Employees.FindAsync(id);
        var employee = await unitOfWork.EmployeesRepository.GetByIdAsync(id);

        if (employee == null)
        {
            throw new WebApiBaseException("Employee not found", StatusCodes.Status404NotFound);
        }

        var response = new ServiceResponse<EmployeeModel>()
        {
            Data = employee
        };

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> UpdateAsync(EditedEmployeeDto editedEmployee)
    {
        var existingEmployee = await unitOfWork.EmployeesRepository.GetByIdAsync(editedEmployee.Id);
        
        if (existingEmployee == null)
        {
            throw new WebApiBaseException("User not found", StatusCodes.Status404NotFound);
        }

        mapper.Map(editedEmployee, existingEmployee);
        existingEmployee.UpdateDate = DateTime.Now.ToLocalTime();

        await unitOfWork.EmployeesRepository.Update(existingEmployee);

        var response = new ServiceResponse<EmployeeModel>()
        {
            Data = existingEmployee
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<EmployeeModel>>> DeleteAsync(int id)
    {
        var employee = await unitOfWork.EmployeesRepository.GetByIdAsync(id);
        if (employee == null)
        {
            throw new WebApiBaseException("User not found", StatusCodes.Status404NotFound);
        }

        unitOfWork.EmployeesRepository.Delete(employee);
        await context.SaveChangesAsync();

        var employeesList = await unitOfWork.EmployeesRepository.GetPaginateAsync(1, 10);
        var response = new ServiceResponse<IEnumerable<EmployeeModel>>()
        {
            Data = employeesList
        };

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> InactivateAsync(int id)
    {
        var employee = await unitOfWork.EmployeesRepository.GetByIdAsync(id);
        if (employee == null)
        {
            throw new WebApiBaseException("User not found", StatusCodes.Status404NotFound);
        }

        employee.Active = false;
        employee.UpdateDate = DateTime.Now.ToLocalTime();

        await unitOfWork.EmployeesRepository.Update(employee);

        var response = new ServiceResponse<EmployeeModel>()
        {
            Data = employee
        };

        return response;
    }
}