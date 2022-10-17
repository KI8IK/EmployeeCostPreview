
namespace EmployeeCostPreview.Dtos.Projection
{
    /// <summary>
    /// A generic payroll line item.
    /// </summary>
    public class PayrollLineItemDto
    {
        /// <summary>
        /// Payroll item description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Dollar amount of the item
        /// </summary>
        public decimal Amount { get; set; } = 0;

        /// <summary>
        /// Year-to-date sum including this item and all prior periods
        /// </summary>
        public decimal? Ytd { get; set; }
    }
}
