using System.ComponentModel.DataAnnotations;

namespace EmployeeCostPreview.Dtos.Dependent
{
    public class AddDependentWithEmployeeIdDto : AddDependentDto
    {
        public int EmployeeId { get; set; }

    }
}
