namespace EmployeeCostPreview.Dtos.Projection
{
    /// <summary>
    /// Period-level cost projections
    /// </summary>
    public class PeriodCostProjectionDto
    {
        /// <summary>
        /// Period number
        /// </summary>
        public short Period { get; set; }

        /// <summary>
        /// Period gross pay
        /// </summary>
        public decimal GrossPay { get; set; } = 0;

        /// <summary>
        /// Period net pay
        /// </summary>
        public decimal NetPay { get; set; } = 0;

        /// <summary>
        /// Collection of earnings line items
        /// </summary>
        public ICollection<PayrollLineItemDto> Earnings { get; set; }

        /// <summary>
        /// Collection of deduction line items
        /// </summary>
        public ICollection<PayrollLineItemDto> Deductions { get; set; }

        /// <summary>
        /// Collection of period-level totals line items
        /// </summary>
        public ICollection<PayrollLineItemDto> PeriodTotals { get; set; }
    }
}
