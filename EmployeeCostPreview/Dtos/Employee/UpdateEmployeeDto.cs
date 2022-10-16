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
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal? PayRate { get; set; }

    }
}
