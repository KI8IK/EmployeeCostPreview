using EmployeeCostPreview.Dtos.Employee;

namespace EmployeeCostPreview.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<GetEmployeeDto>> GetById(int id);
        Task<ServiceResponse<List<GetEmployeeDto>>> GetAll();
        Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee);
        Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updateEmployee);
        Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id);
    }
}
