using System.ComponentModel.DataAnnotations;

namespace EmployeeCostPreview.Dtos.Employee
{
    /// <summary>
    /// DTO used for employee PUT operations.
    /// All non-Id fields are optional so that only changed
    /// fields need be passed.
    /// </summary>
    public class UpdateEmployeeDto
    {
        [Required]
        /// <summary>
        /// Employee id - Required
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Updated employee first name - Optional
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Updated employee last name - Optional
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Updated employee pay rate - Optional
        /// </summary>
        public decimal? PayRate { get; set; }
    }
}
