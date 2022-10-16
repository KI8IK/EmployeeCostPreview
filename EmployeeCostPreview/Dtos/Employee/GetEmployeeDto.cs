using EmployeeCostPreview.Dtos.Dependent;

namespace EmployeeCostPreview.Dtos.Employee
{
    /// <summary>
    /// DTO used for employee GET operations (basically any 
    /// employee object which is returned by the API)
    /// </summary>
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal PayRate { get; set; }

        public ICollection<GetDependentDto> Dependents { get; set; }
    }
}
