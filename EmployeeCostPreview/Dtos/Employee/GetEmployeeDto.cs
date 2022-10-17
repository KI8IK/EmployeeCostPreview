using EmployeeCostPreview.Dtos.Dependent;

namespace EmployeeCostPreview.Dtos.Employee
{
    /// <summary>
    /// DTO used for employee GET operations (basically any 
    /// employee object which is returned by the API)
    /// </summary>
    public class GetEmployeeDto
    {
        /// <summary>
        /// Employee id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Employee first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee last name
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        /// Employee pay rate
        /// </summary>
        public decimal PayRate { get; set; }

        /// <summary>
        /// Collection of related dependents
        /// </summary>
        public ICollection<GetDependentDto> Dependents { get; set; }
    }
}
