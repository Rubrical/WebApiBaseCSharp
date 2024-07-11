using Microsoft.EntityFrameworkCore;
using WebApiBase.Data;
using WebApiBase.Enums;
using WebApiBase.Models;

namespace WebApiBase.Services.EmployeeService;

public class EmployeeService(AppDbContext context) : IEmployeeService
{
    public async Task<ServiceResponse<List<EmployeeModel>>> GetAllAsync()
    {
        var response = new ServiceResponse<List<EmployeeModel>>();
        try
        {
            response.Data = await context.Employees.AsNoTracking().ToListAsync();
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

            if (!Enum.IsDefined(typeof(ShiftEnum), newEmployee.Shift) ||
                !Enum.IsDefined(typeof(DepartmentEnum), newEmployee.Department)
               )
            {
                throw new InvalidDataException("Invalid shift or department");
            }

            var now = DateTime.Now.ToLocalTime();
            newEmployee.DateCreation = now;
            newEmployee.UpdateDate = now;

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

    public async Task<ServiceResponse<EmployeeModel>> UpdateAsync(EmployeeModel editedEmployee)
    {
        var response = new ServiceResponse<EmployeeModel>();
        try
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == editedEmployee.Id);

            if (employee == null)
            {
                throw new InvalidDataException("User not found");
            }

            employee.Name = editedEmployee.Name;
            employee.LastName = editedEmployee.LastName;
            employee.Active = editedEmployee.Active;
            employee.Department = editedEmployee.Department;
            employee.Shift = editedEmployee.Shift;
            
            editedEmployee.UpdateDate = DateTime.Now.ToLocalTime();

            await context.SaveChangesAsync();

            response.Data = editedEmployee;
        }
        catch (Exception e)
        {
            response.Data = null;
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<List<EmployeeModel>>> DeleteAsync(int id)
    {
        var response = new ServiceResponse<List<EmployeeModel>>();
        try
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                throw new InvalidDataException("User not found");
            }

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();

            response.Data = await context.Employees.AsNoTracking().ToListAsync();
        }
        catch (Exception e)
        {
            response.Data = null;
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<EmployeeModel>> InactivateAsync(int id)
    {
        var response = new ServiceResponse<EmployeeModel>();
        try
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                throw new InvalidDataException("User not found");
            }

            employee.Active = false;
            employee.UpdateDate = DateTime.Now.ToLocalTime();

            // context.Employees.Update(employee);
            await context.SaveChangesAsync();

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
}