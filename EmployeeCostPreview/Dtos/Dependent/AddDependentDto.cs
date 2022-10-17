using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = Constants.S_Error_FirstNameRequired), MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Dependent last name
        /// </summary>
        [Required(ErrorMessage = Constants.S_Error_LastNameRequired), MaxLength(50)]
        public string LastName { get; set; }
    }
}
