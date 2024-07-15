using Microsoft.AspNetCore.Mvc;
using WebApiBase.Models;
using WebApiBase.Services.EmployeeService;

namespace WebApiBase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var response = await employeeService.GetAllAsync();
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeModel), 201)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] EmployeeModel employeeModel)
    {
        var response = await employeeService.CreateNewAsync(employeeModel);
        return Created($"api/Employees/{employeeModel.Id}", response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await employeeService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(EmployeeModel employeeModel)
    {
        var response = await employeeService.UpdateAsync(employeeModel);
        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await employeeService.DeleteAsync(id);
        return Ok(response);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InactivateAsync(int id)
    {
        var response = await employeeService.InactivateAsync(id);
        return Ok(response);
    }
    
}