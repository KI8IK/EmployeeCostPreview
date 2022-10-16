using EmployeeCostPreview.Dtos.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCostPreview.Dtos.Dependent
{
    /// <summary>
    /// DTO use for dependent POST operations
    /// </summary>
    public class AddDependentDto
    {
        /// <summary>
        /// Dependent first name
        /// </summary>
        [Required(ErrorMessage = "A first name is requied"), MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Dependent last name
        /// </summary>
        [Required(ErrorMessage = "A last name is requied"), MaxLength(50)]
        public string LastName { get; set; }
    }
}
