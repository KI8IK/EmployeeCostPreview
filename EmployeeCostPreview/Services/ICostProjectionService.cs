using EmployeeCostPreview.Dtos.Projection;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCostPreview.Services
{
    /// <summary>
    /// Cost projection service interface
    /// </summary>
    public interface ICostProjectionService
    {
        Task<ServiceResponse<FiscalYearCostProjectionDto>> CalculateFiscalYearCostProjectionsForEmployee(int id);
    }
}
