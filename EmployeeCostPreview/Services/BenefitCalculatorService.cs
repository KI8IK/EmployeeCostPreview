using EmployeeCostPreview.Dtos.Projection;

namespace EmployeeCostPreview.Services
{
    /// <summary>
    /// Service for calculating benefit deduction information
    /// </summary>
    public class BenefitCalculatorService : IBenefitCalculatorService
    {
        // Static dictionaries which contain the benefit cost schedules
        // for each period of the fiscal year. 
        static Dictionary<short, decimal>? _employeeBenefitPaymentSchedule;
        static Dictionary<short, decimal>? _employeeDiscountBenefitPaymentSchedule;
        static Dictionary<short, decimal>? _dependentBenefitPaymentSchedule;
        static Dictionary<short, decimal>? _dependentDiscountBenefitPaymentSchedule;

        /// <summary>
        /// Determines if benefit discounts apply to an individual
        /// </summary>
        /// <param name="firstName">First name of an employee or dependent</param>
        /// <returns>True if the benefits should be discounted for the related individual</returns>
        public bool IsDiscountable(string firstName)
        {
            return firstName.ToUpper().StartsWith('A');
        }

        /// <summary>
        /// Calculate the benefit deductions for the given employee and period.
        /// </summary>
        /// <param name="dbEmployee">Employee model</param>
        /// <param name="period">Period to be calculated</param>
        /// <returns>Collection of line items containing the benefit deductions for the period</returns>
        ICollection<PayrollLineItemDto> IBenefitCalculatorService.CalculatePeriodBenefitDeductions(Employee dbEmployee, ICollection<PeriodCostProjectionDto> priorPeriods, short period)
        {
            ICollection<PayrollLineItemDto> benefitDeductions = new List<PayrollLineItemDto>();

            // Add employee benefit deduction
            var isDiscountable = IsDiscountable(dbEmployee.FirstName);
            var benefitPaymentSchedule = GetBenefitPaymentSchedule(true, isDiscountable);
            benefitDeductions.Add(new PayrollLineItemDto
            {
                Description = Constants.S_EmployeeBenefits,
                Amount = benefitPaymentSchedule[period],
                // Ytd is the sum of all prior period employee deductions plus the current period's.
                Ytd = priorPeriods.Sum(p => p.Deductions
                    .Where(e => e.Description == Constants.S_EmployeeBenefits)
                    .Sum(e => e.Amount))
                    + benefitPaymentSchedule[period]
            });

            // Add dependent benefit deductions
            if (dbEmployee.Dependents.Any())
            {
                // Total the benefit dedutions for all dependents,
                // we only show a single line item for dependent benefits.
                decimal totalDependentBenefitDeduction = 0m;
                foreach (var dependent in dbEmployee.Dependents)
                {
                    isDiscountable = IsDiscountable(dependent.FirstName);
                    benefitPaymentSchedule = GetBenefitPaymentSchedule(false, isDiscountable);
                    totalDependentBenefitDeduction += benefitPaymentSchedule[period];
                }
                // Add the dependents benefit deduction line item
                benefitDeductions.Add(new PayrollLineItemDto
                {
                    Description = Constants.S_DependentBenefits,
                    Amount = totalDependentBenefitDeduction,
                    // Ytd is the sum of all prior period dependent deductions plus the current period's.
                    Ytd = priorPeriods.Sum(p => p.Deductions
                        .Where(e => e.Description == Constants.S_DependentBenefits)
                        .Sum(e => e.Amount))
                        + totalDependentBenefitDeduction
                });
            }
            return benefitDeductions;
        }

        /// <summary>
        /// Applicable benefit payment schedule for the individual
        /// </summary>
        /// <param name="isEmployee">Pass true if the individual is an employee</param>
        /// <param name="isDiscountable">Pass true if the individual's benefits should receive a discount</param>
        /// <returns>A dictionary containing the individual's benefit deduction amount for each period. i.e. Dictionary<period, amount> </returns>
        private Dictionary<short, decimal> GetBenefitPaymentSchedule(bool isEmployee, bool isDiscountable)
        {
            if (isEmployee)
            {
                if (isDiscountable)
                {
                    if (_employeeDiscountBenefitPaymentSchedule == null)
                    {
                        // Generate the employee discounted benefit payment schedule
                        var discountedBenefitCost = Constants.FiscalYearEmployeeBenefitCost - (Constants.FiscalYearEmployeeBenefitCost * Constants.BenefitDiscountRate);
                        _employeeDiscountBenefitPaymentSchedule = GenerateBenefitPaymentSchedule(discountedBenefitCost);
                    }
                    return _employeeDiscountBenefitPaymentSchedule;
                }
                else
                {
                    if (_employeeBenefitPaymentSchedule == null)
                    {
                        // Generate the employee standard benefit payment schedule
                        _employeeBenefitPaymentSchedule = GenerateBenefitPaymentSchedule(Constants.FiscalYearEmployeeBenefitCost);
                    }
                    return _employeeBenefitPaymentSchedule;
                }
            }
            else
            {
                if (isDiscountable)
                {
                    if (_dependentDiscountBenefitPaymentSchedule == null)
                    {
                        // Generate the dependent discounted benefit payment schedule
                        var discountedBenefitCost = Constants.FiscalYearDependentBenefitCost - (Constants.FiscalYearDependentBenefitCost * Constants.BenefitDiscountRate);
                        _dependentDiscountBenefitPaymentSchedule = GenerateBenefitPaymentSchedule(discountedBenefitCost);
                    }
                    return _dependentDiscountBenefitPaymentSchedule;
                }
                else
                {
                    if (_dependentBenefitPaymentSchedule == null)
                    {
                        // Generate the dependent standard benefit payment schedule
                        _dependentBenefitPaymentSchedule = GenerateBenefitPaymentSchedule(Constants.FiscalYearDependentBenefitCost);
                    }
                    return _dependentBenefitPaymentSchedule;
                }
            }
        }

        /// <summary>
        /// Generates the benefit payment schedule for the given yearly benefit cost
        /// </summary>
        /// <param name="yearlyBenefitCost">The total yearly cost for the given benefit</param>
        /// <returns>A Dictionary containing a mapping of period number to benefit deduction amount</returns>
        private Dictionary<short, decimal> GenerateBenefitPaymentSchedule(decimal yearlyBenefitCost)
        {
            var paymentSchedule = new Dictionary<short, decimal>();
            var remainingCost = yearlyBenefitCost;
            var remainingPeriods = Constants.FiscalYearPeriods;

            for (short period = 1; period <= Constants.FiscalYearPeriods; period++)
            {
                var periodCost = Math.Round(remainingCost / remainingPeriods, 2);
                paymentSchedule.Add(period, periodCost);
                remainingCost -= periodCost;
                remainingPeriods--;
            }
            return paymentSchedule;
        }
    }
}
