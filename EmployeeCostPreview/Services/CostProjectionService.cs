using AutoMapper;
using EmployeeCostPreview.Data;
using EmployeeCostPreview.Dtos.Employee;
using EmployeeCostPreview.Dtos.Projection;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCostPreview.Services
{
    /// <summary>
    /// Service which calculates cost projections
    /// </summary>
    public class CostProjectionService : ICostProjectionService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IBenefitCalculatorService _benefitCalculatorService;

        public CostProjectionService(IMapper mapper, DataContext context, IBenefitCalculatorService benefitCalculatorService)
        {
            _mapper = mapper;
            _context = context;
            _benefitCalculatorService = benefitCalculatorService;
        }

        /// <summary>
        /// Calculate the fiscal year cost projections for the given employee id
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>Period cost projections for a fiscal year for the given employee</returns>
        public async Task<ServiceResponse<FiscalYearCostProjectionDto>> CalculateFiscalYearCostProjectionsForEmployee(int id)
        {
            var serviceResponse = new ServiceResponse<FiscalYearCostProjectionDto>();
            try
            {
                // Retrieve the employee data model
                var dbEmployee = await _context.Employees
                    .Include(e => e.Dependents)
                    .FirstAsync(e => e.Id == id);

                // Generate the cost projections for all periods of a fiscal year
                var fyProjection = new FiscalYearCostProjectionDto();
                fyProjection.Employee = _mapper.Map<GetEmployeeDto>(dbEmployee);
                fyProjection.Periods = CalculateAllPeriodProjections(dbEmployee);
                fyProjection.FiscalYearTotals = CalculateFiscalYearTotals(fyProjection.Periods);

                serviceResponse.Data = fyProjection;
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Constants.S_Error_EmployeeNotFoundMessage;
                serviceResponse.Error = $"{ex.GetType()} - An employee with an id of '{id}' could not be found.";
            }
            return (serviceResponse);
        }

        /// <summary>
        /// Calculate the fiscal year totals for earnings and deductions 
        /// </summary>
        /// <param name="periods"></param>
        /// <returns></returns>
        private ICollection<PayrollLineItemDto> CalculateFiscalYearTotals(ICollection<PeriodCostProjectionDto> periods)
        {
            var fiscalYearTotals = new List<PayrollLineItemDto>();

            // Add a fiscal year totals line item for earnings
            var fyEarnings = periods.Sum(p => p.Earnings.Sum(c => c.Amount));
            fiscalYearTotals.Add(new PayrollLineItemDto
            {
                
                Description = Constants.S_Earnings,
                Amount = fyEarnings,
                Ytd = fyEarnings
            });

            // Add a fiscal year totals line item for deductions
            var fyDeductions = periods.Sum(p => p.Deductions.Sum(c => c.Amount));
            fiscalYearTotals.Add(new PayrollLineItemDto
            {
                Description = Constants.S_Deductions,
                Amount = fyDeductions,
                Ytd = fyDeductions
            });

            return fiscalYearTotals;
        }

        /// <summary>
        /// Calcualte the cost projections for all payroll periods for the given employee
        /// </summary>
        /// <param name="dbEmployee">Employee data model</param>
        /// <returns>The collection of period cost projections</returns>
        private ICollection<PeriodCostProjectionDto> CalculateAllPeriodProjections(Employee dbEmployee)
        {
            ICollection<PeriodCostProjectionDto> projections = new List<PeriodCostProjectionDto>();

            // Calculate the cost projections for each payroll period
            for (short period = 1; period <= Constants.FiscalYearPeriods; period++)
            {
                projections.Add(CalculatePeriodProjection(dbEmployee, projections, period));
            }

            return projections;
        }

        /// <summary>
        /// Calculates the cost projections for a given period
        /// </summary>
        /// <param name="dbEmployee">The employee model</param>
        /// <param name="priorPeriods">The cost projections for all prior periods</param>
        /// <param name="period">The period number to be calculate</param>
        /// <returns>Cost projections for the requested period</returns>
        private PeriodCostProjectionDto CalculatePeriodProjection(Employee dbEmployee, ICollection<PeriodCostProjectionDto> priorPeriods, short period)
        {
            // Period cost projection object that will be filled and returned
            PeriodCostProjectionDto projection = new PeriodCostProjectionDto{ Period = period };

            // Add the employee earnings to the period cost projection
            projection.Earnings = new List<PayrollLineItemDto>
            {
                new PayrollLineItemDto{
                    Description = Constants.S_Wages,
                    Amount = dbEmployee.PayRate,
                    // Ytd is the sum of all prior period wages plus the current period's wages.
                    Ytd = priorPeriods.Sum(p => p.Earnings
                    .Where(e => e.Description == Constants.S_Wages)
                    .Sum(e => e.Amount)) 
                    + dbEmployee.PayRate
                }
            };

            // Calculate the gross pay from the sum of the period earnings
            var totalEarnings = projection.Earnings.Sum(e => e.Amount);
            projection.GrossPay = totalEarnings;

            // Add the benefit deductions to the period cost projection
            projection.Deductions = 
                _benefitCalculatorService.CalculatePeriodBenefitDeductions(dbEmployee, priorPeriods, period);

            // Calculate the net pay by subtracting the sum of the period deductions
            // from the gross pay
            var totalDeductions = projection.Deductions.Sum(e => e.Amount);

            // Calculate the period net pay
            projection.NetPay = totalEarnings - totalDeductions;

            // Add period totals for earnings and deductions
            projection.PeriodTotals = new List<PayrollLineItemDto>
            { 
                // Add a period total earnings line item 
                new PayrollLineItemDto
                {
                    Description = Constants.S_Earnings,
                    Amount = totalEarnings,
                    // Ytd is the sum of all prior period earning totals plus the current period's totals.
                    Ytd = priorPeriods.Sum(p => p.PeriodTotals
                    .Where(e => e.Description == Constants.S_Earnings)
                    .Sum(e => e.Amount))
                    + totalEarnings
                },

                // Add a period total deductions line item
                new PayrollLineItemDto
                {
                    Description = Constants.S_Deductions,
                    Amount = totalDeductions,
                    // Ytd is the sum of all prior period deduction totals plus the current period's totals.
                    Ytd = priorPeriods.Sum(p => p.PeriodTotals
                    .Where(e => e.Description == Constants.S_Deductions)
                    .Sum(e => e.Amount))
                    + totalDeductions                
                }
            };
            return projection;
        }
    }
}
