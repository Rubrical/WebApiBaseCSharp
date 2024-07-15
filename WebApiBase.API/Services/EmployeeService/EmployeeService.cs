using Microsoft.EntityFrameworkCore;
using WebApiBase.Data;
using WebApiBase.Enums;
using WebApiBase.Exceptions;
using WebApiBase.Models;

namespace WebApiBase.Services.EmployeeService;

public class EmployeeService(AppDbContext context) : IEmployeeService
{
    public async Task<ServiceResponse<List<EmployeeModel>>> GetAllAsync()
    {
        var employees = await context.Employees.AsNoTracking().ToListAsync();

        if (employees is null)
        {
            throw new WebApiBaseException("It was not possible to connect to database", StatusCodes.Status500InternalServerError);
        }

        if (employees.Count == 0)
        {
            throw new WebApiBaseException("No registries found", StatusCodes.Status500InternalServerError);
        }

        var response = new ServiceResponse<List<EmployeeModel>>
        {
            Data = employees,
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
        var employee = await context.Employees.FindAsync(id);
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

    public async Task<ServiceResponse<EmployeeModel>> UpdateAsync(EmployeeModel editedEmployee)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == editedEmployee.Id);

        if (employee == null)
        {
            throw new WebApiBaseException("User not found", StatusCodes.Status404NotFound);
        }

        employee.Name = editedEmployee.Name;
        employee.LastName = editedEmployee.LastName;
        employee.Active = editedEmployee.Active;
        employee.Department = editedEmployee.Department;
        employee.Shift = editedEmployee.Shift;

        editedEmployee.UpdateDate = DateTime.Now.ToLocalTime();

        await context.SaveChangesAsync();

        var response = new ServiceResponse<EmployeeModel>()
        {
            Data = editedEmployee
        };

        return response;
    }

    public async Task<ServiceResponse<List<EmployeeModel>>> DeleteAsync(int id)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            throw new WebApiBaseException("User not found", StatusCodes.Status404NotFound);
        }

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();

        var employeesList = await context.Employees.AsNoTracking().ToListAsync();
        var response = new ServiceResponse<List<EmployeeModel>>()
        {
            Data = employeesList
        };

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> InactivateAsync(int id)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            throw new WebApiBaseException("User not found", StatusCodes.Status404NotFound);
        }

        employee.Active = false;
        employee.UpdateDate = DateTime.Now.ToLocalTime();

        await context.SaveChangesAsync();

        var response = new ServiceResponse<EmployeeModel>()
        {
            Data = employee
        };

        return response;
    }
}