using AutoMapper;
using EmployeeCostPreview.Dtos.Dependent;
using EmployeeCostPreview.Dtos.Employee;

namespace EmployeeCostPreview
{
    /// <summary>
    /// Mapping definitions which define the property conversions between
    /// models and DTOs.
    /// </summary>
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Employee, GetEmployeeDto>();

            // When mapping the AddEmployeeDto to an Employee, inject the
            // default employee pay rate if a pay rate was not defined.
            CreateMap<AddEmployeeDto, Employee>()
                .ForMember(dest => dest.PayRate, opt => opt.NullSubstitute(Constants.DefaultEmployeePayRate))
                .ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Dependents));

            // When mapping the UpdateEmployeeDto to an Employee, only map
            // the fields which are included in the request body. This allows
            // the user to only pass the fields which are changing.
            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.FirstName, opt => opt.Condition(dest => dest.FirstName != null))
                .ForMember(dest => dest.LastName, opt => opt.Condition(dest => dest.LastName != null))
                .ForMember(dest => dest.PayRate, opt => opt.Condition(dest => dest.PayRate != null));

            CreateMap<Dependent, GetDependentDto>();

            CreateMap<AddDependentDto, Dependent>();

            CreateMap<AddDependentWithEmployeeIdDto, Dependent>();

            // When mapping the UpdateDependentDto to an Dependent, only map
            // the fields which are included in the request body. This allows
            // the user to only pass the fields which are changing.
            CreateMap<UpdateDependentDto, Dependent>()
                .ForMember(dest => dest.FirstName, opt => opt.Condition(dest => dest.FirstName != null))
                .ForMember(dest => dest.LastName, opt => opt.Condition(dest => dest.LastName != null));
        }
    }
}
