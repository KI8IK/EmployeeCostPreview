namespace EmployeeCostPreview
{

    public class Constants
    {
        /// <summary>
        /// The default period pay rate for an employee
        /// </summary>
        public const decimal DefaultEmployeePayRate = 2000.00m;

        /// <summary>
        /// The number of pay periods in a fiscal year
        /// </summary>
        public const short FiscalYearPeriods = 26;

        /// <summary>
        /// The yearly cost of benefits for an employee
        /// </summary>
        public const decimal FiscalYearEmployeeBenefitCost = 1000.00m;

        /// <summary>
        /// The yearly cost of benefits for an employee dependent
        /// </summary>
        public const decimal FiscalYearDependentBenefitCost = 500.00m;

        /// <summary>
        /// The discount rate that is applied to the yearly benefits cost 
        /// for employees and dependents who qualify for a discount.
        /// </summary>
        public const decimal BenefitDiscountRate = 0.10m;

        /// <summary>
        /// String constants
        /// </summary>
         
        public const string S_Wages = "Wages";
        public const string S_Benefit = "Benefits";
        public const string S_Discounted = "Discounted";
        public const string S_Earnings = "Earnings";
        public const string S_Deductions = "Deductions";
        public const string S_EmployeeBenefits = $"Employee {S_Benefit}";
        public const string S_DependentBenefits = $"Dependent {S_Benefit}";
        public const string S_Error_FirstNameRequired = "A first name is requied";
        public const string S_Error_LastNameRequired = "A last name is requied";
        public const string S_Error_EmployeeNotFoundMessage = "Employee not found";
        public const string S_Error_DependentNotFoundMessage = "Dependent not found";
    }
}
