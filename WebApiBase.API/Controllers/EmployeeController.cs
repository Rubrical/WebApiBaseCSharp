using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiBase.Data.DTOs;
using WebApiBase.Models;
using WebApiBase.Services.EmployeeService;

namespace WebApiBase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Resgata todos os empregados do banco por páginas
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaginateAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var response = await employeeService.GetPaginateAsync(pageNumber, pageSize);
        return Ok(response);
    }

    /// <summary>
    /// Cadastra novo empregado
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(EmployeeModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromQuery] EmployeeDTO employee)
    {
        var newEmployee = mapper.Map<EmployeeModel>(employee);
        var response = await employeeService.CreateNewAsync(newEmployee);
        
        return Created($"api/employee/{response.Data.Id}", response);
    }

    /// <summary>
    /// Resgata empregado por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await employeeService.GetByIdAsync(id);
        return Ok(response);
    }
    
    /// <summary>
    /// Atualiza empregado
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(EditedEmployeeDto editedEmployeeDto)
    {
        // var employeeModel = mapper.Map<EmployeeModel>(editedEmployeeDto);
        var response = await employeeService.UpdateAsync(editedEmployeeDto);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Deleta empregado
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await employeeService.DeleteAsync(id);
        return Ok(response);
    }
    
    /// <summary>
    /// Inativa empregado
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InactivateAsync(int id)
    {
        var response = await employeeService.InactivateAsync(id);
        return Ok(response);
    }
    
}