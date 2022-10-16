using EmployeeCostPreview.Dtos.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCostPreview.Dtos.Dependent
{
    /// <summary>
    /// DTO used for dependent GET operations (basically any 
    /// dependent object which is returned by the API)
    /// </summary>    
    public class GetDependentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
