using EmployeeCostPreview.Dtos.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCostPreview.Dtos.Dependent
{
    /// <summary>
    /// DTO used for dependent PUT operations.
    /// All non-Id fields are optional so that only changed
    /// fields need be passed.
    /// </summary>
    public class UpdateDependentDto
    {
        [Required]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
