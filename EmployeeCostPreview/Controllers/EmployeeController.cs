using EmployeeCostPreview.Dtos.Employee;
using EmployeeCostPreview.Models;
using EmployeeCostPreview.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCostPreview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> Get(int id)
        {
            var serviceResponse = await _employeeService.GetById(id);
            return serviceResponse.Success ? Ok(serviceResponse) : NotFound(serviceResponse);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDto>>>> GetAll()
        {
            return Ok(await _employeeService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddEmployeeDto>>>> AddEmployee(AddEmployeeDto newEmployee)
        { 
            var serviceResponse = await _employeeService.AddEmployee(newEmployee);
            return serviceResponse.Success ? Ok(serviceResponse) : Conflict(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> UpdateEmployee(UpdateEmployeeDto updateEmployee)
        {
            var serviceResponse = await _employeeService.UpdateEmployee(updateEmployee);
            return serviceResponse.Success
                ? Ok(serviceResponse) : (serviceResponse.Message == EmployeeService.EmployeeNotFoundMessage
                ? NotFound(serviceResponse)
                : Conflict(serviceResponse));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDto>>>> DeleteEmployee(int id)
        {
            var serviceResponse = await _employeeService.DeleteEmployee(id);
            return serviceResponse.Success
                ? Ok(serviceResponse) : (serviceResponse.Message == EmployeeService.EmployeeNotFoundMessage
                ? NotFound(serviceResponse)
                : Conflict(serviceResponse));
        }
    }
}
