using System.ComponentModel.DataAnnotations;

namespace EmployeeCostPreview.Dtos.Dependent
{
    /// <summary>
    /// DTO used for dependent POST operations when an employee FK is required
    /// </summary>
    public class AddDependentWithEmployeeIdDto : AddDependentDto
    {
        /// <summary>
        /// Employee id FK
        /// </summary>
        public int EmployeeId { get; set; }

    }
}
