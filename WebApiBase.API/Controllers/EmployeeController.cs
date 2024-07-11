using Microsoft.AspNetCore.Mvc;
using WebApiBase.Models;
using WebApiBase.Services.EmployeeService;

namespace WebApiBase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await employeeService.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeModel employeeModel)
    {
        return Ok(await employeeService.CreateNewAsync(employeeModel));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await employeeService.GetByIdAsync(id);
        return Ok(response);
    }
}