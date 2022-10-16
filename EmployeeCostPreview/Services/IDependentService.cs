using EmployeeCostPreview.Dtos.Dependent;

namespace EmployeeCostPreview.Services
{
    public interface IDependentService
    {
        Task<ServiceResponse<GetDependentDto>> GetById(int id);
        Task<ServiceResponse<List<GetDependentDto>>> GetAll();
        Task<ServiceResponse<List<GetDependentDto>>> AddDependent(AddDependentDto newDependent);
        Task<ServiceResponse<GetDependentDto>> UpdateDependent(UpdateDependentDto updateDependent);
        Task<ServiceResponse<List<GetDependentDto>>> DeleteDependent(int id);
    }
}
