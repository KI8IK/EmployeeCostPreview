using EmployeeCostPreview.Dtos.Dependent;
using EmployeeCostPreview.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCostPreview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DependentController : ControllerBase
    {
        private readonly IDependentService _DependentService;

        public DependentController(IDependentService DependentService)
        {
            this._DependentService = DependentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetDependentDto>>> Get(int id)
        {
            var serviceResponse = await _DependentService.GetById(id);
            return serviceResponse.Success ? Ok(serviceResponse) : NotFound(serviceResponse);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetDependentDto>>>> GetAll()
        {
            return Ok(await _DependentService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddDependentWithEmployeeIdDto>>>> AddDependent(AddDependentWithEmployeeIdDto newDependent)
        {
            var serviceResponse = await _DependentService.AddDependent(newDependent);
            return serviceResponse.Success ? Ok(serviceResponse) : Conflict(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetDependentDto>>> UpdateDependent(UpdateDependentDto updateDependent)
        {
            var serviceResponse = await _DependentService.UpdateDependent(updateDependent);
            return serviceResponse.Success
                ? Ok(serviceResponse) : (serviceResponse.Message == DependentService.DependentNotFoundMessage
                ? NotFound(serviceResponse)
                : Conflict(serviceResponse));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetDependentDto>>>> DeleteDependent(int id)
        {
            var serviceResponse = await _DependentService.DeleteDependent(id);
            return serviceResponse.Success 
                ? Ok(serviceResponse) : (serviceResponse.Message == DependentService.DependentNotFoundMessage 
                ? NotFound(serviceResponse)
                : Conflict(serviceResponse));
        }
    }
}
