using EmployeeCostPreview.Dtos.Dependent;
using EmployeeCostPreview.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(Summary = "Returns a specific dependent by key")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetDependentDto>>> Get(int id)
        {
            var serviceResponse = await _DependentService.GetById(id);
            return serviceResponse.Success ? Ok(serviceResponse) : NotFound(serviceResponse);
        }

        [SwaggerOperation(Summary = "Returns the existing dependent collection")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetDependentDto>>>> GetAll()
        {
            return Ok(await _DependentService.GetAll());
        }

        [SwaggerOperation(Summary = "Adds a new dependent to the dependent collection")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddDependentWithEmployeeIdDto>>>> AddDependent(AddDependentWithEmployeeIdDto newDependent)
        {
            var serviceResponse = await _DependentService.AddDependent(newDependent);
            return serviceResponse.Success ? Ok(serviceResponse) : Conflict(serviceResponse);
        }

        [SwaggerOperation(Summary = "Updates an existing dependent")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetDependentDto>>> UpdateDependent(UpdateDependentDto updateDependent)
        {
            var serviceResponse = await _DependentService.UpdateDependent(updateDependent);
            return serviceResponse.Success
                ? Ok(serviceResponse) : (serviceResponse.Message == Constants.S_Error_DependentNotFoundMessage
                ? NotFound(serviceResponse)
                : Conflict(serviceResponse));
        }

        [SwaggerOperation(Summary = "Removes a specific dependent from the dependent collection")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetDependentDto>>>> DeleteDependent(int id)
        {
            var serviceResponse = await _DependentService.DeleteDependent(id);
            return serviceResponse.Success 
                ? Ok(serviceResponse) : (serviceResponse.Message == Constants.S_Error_DependentNotFoundMessage
                ? NotFound(serviceResponse)
                : Conflict(serviceResponse));
        }
    }
}
