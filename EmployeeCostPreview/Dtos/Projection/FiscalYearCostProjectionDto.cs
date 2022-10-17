using EmployeeCostPreview.Dtos.Employee;

namespace EmployeeCostPreview.Dtos.Projection
{
    /// <summary>
    /// Cost projections for a given employee
    /// </summary>
    public class FiscalYearCostProjectionDto
    {
        /// <summary>
        /// Employee record
        /// </summary>
        public GetEmployeeDto Employee { get; set; }
        
        /// <summary>
        /// Collection of period projections
        /// </summary>
        public ICollection<PeriodCostProjectionDto> Periods { get; set; }

        /// <summary>
        /// Collection of fiscal year totals
        /// </summary>
        public ICollection<PayrollLineItemDto> FiscalYearTotals { get; set; }
    }
}
