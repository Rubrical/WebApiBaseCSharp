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
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeModel), 201)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] EmployeeModel employeeModel)
    {
        var response = await employeeService.CreateNewAsync(employeeModel);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Created($"api/Employees/{employeeModel.Id}", response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await employeeService.GetByIdAsync(id);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(EmployeeModel employeeModel)
    {
        var response = await employeeService.UpdateAsync(employeeModel);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await employeeService.DeleteAsync(id);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InactivateAsync(int id)
    {
        var response = await employeeService.InactivateAsync(id);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
    
}