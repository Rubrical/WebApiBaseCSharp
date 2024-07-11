using Microsoft.EntityFrameworkCore;
using WebApiBase.Data;
using WebApiBase.Models;

namespace WebApiBase.Services.EmployeeService;

public class EmployeeService(AppDbContext context) : IEmployeeService
{
    public async Task<ServiceResponse<List<EmployeeModel>>> GetAllAsync()
    {
        var response = new ServiceResponse<List<EmployeeModel>>();
        try
        {
            response.Data = await context.Employees.ToListAsync();
            response.Success = true;

            if (response.Data.Count == 0)
            {
                response.Message = "No registries found";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> CreateNewAsync(EmployeeModel newEmployee)
    {
        var response = new ServiceResponse<EmployeeModel>();
        try
        {
            if (newEmployee.Name == string.Empty || newEmployee.LastName == string.Empty)
            {
                throw new InvalidDataException("Empty name and/or last name");
            }

            context.Add(newEmployee);
            await context.SaveChangesAsync();
            response.Data = newEmployee;
        }
        catch (Exception e)
        {
            response.Data = null;
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> GetByIdAsync(int id)
    {
        var response = new ServiceResponse<EmployeeModel>();
        try
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new InvalidDataException("Employee not found");
            }
            response.Data = employee;
        }
        catch (Exception e)
        {
            response.Data = null;
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public Task<ServiceResponse<EmployeeModel>> UpdateAsync(EmployeeModel employeeModel)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<EmployeeModel>>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<EmployeeModel>> InactivateAsync(int id)
    {
        throw new NotImplementedException();
    }
}