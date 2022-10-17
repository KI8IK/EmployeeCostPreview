using EmployeeCostPreview.Dtos.Projection;

namespace EmployeeCostPreview.Services
{
    /// <summary>
    /// Benefit calculator service interface
    /// </summary>
    public interface IBenefitCalculatorService
    {
        ICollection<PayrollLineItemDto> CalculatePeriodBenefitDeductions(Employee dbEmployee, ICollection<PeriodCostProjectionDto> priorPeriods, short period);
        bool IsDiscountable(string firstName);
    }
}
