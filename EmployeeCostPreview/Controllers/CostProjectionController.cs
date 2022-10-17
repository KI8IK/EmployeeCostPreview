using EmployeeCostPreview.Dtos.Projection;
using EmployeeCostPreview.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeeCostPreview.Controllers
{
    /// <summary>
    /// Retrieval of cost projections
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CostProjectionController : Controller
    {
        private readonly ICostProjectionService _costProjectionService;

        public CostProjectionController(ICostProjectionService costProjectionService)
        {
            _costProjectionService = costProjectionService;
        }

        /// <summary>
        /// Employee cost projections for all periods of a fiscal year
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Service response containing employee cost projections</returns>
        [SwaggerOperation(Summary = "Generates a fiscal year cost projection for a given employee")]
        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<ServiceResponse<FiscalYearCostProjectionDto>>> GetEmployee(int id)
        {
            var serviceResponse = await _costProjectionService.CalculateFiscalYearCostProjectionsForEmployee(id);
            return serviceResponse.Success ? Ok(serviceResponse) : NotFound(serviceResponse);
        }
    }
}
