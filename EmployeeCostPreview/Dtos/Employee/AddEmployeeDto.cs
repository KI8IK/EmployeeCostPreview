using EmployeeCostPreview.Dtos.Dependent;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCostPreview.Dtos.Employee
{
    /// <summary>
    /// DTO use for employee POST operations
    /// </summary>
    public class AddEmployeeDto
    {
        /// <summary>
        /// Employee first name
        /// </summary>
        [Required(ErrorMessage = "A first name is requied"), MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Employee last name
        /// </summary>
        [Required(ErrorMessage = "A last name is requied"), MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Optional employee pay rate.
        /// If a pay rate is not defined when the employee
        /// is created, the employee will receive a system
        /// defined default pay rate.
        /// </summary>
        public decimal? PayRate { get; set; }

        /// <summary>
        /// Collection of related dependents
        /// </summary>
        public ICollection<AddDependentDto> Dependents { get; set; }

    }
}
