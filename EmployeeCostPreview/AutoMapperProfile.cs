using AutoMapper;
using EmployeeCostPreview.Dtos.Dependent;
using EmployeeCostPreview.Dtos.Employee;
using System.Reflection.PortableExecutable;

namespace EmployeeCostPreview
{
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// The default employee pay rate an employee receives if one is not provided
        /// during the employee POST operation.
        /// </summary>
        public const decimal _defaultPayRate = 2000.00m;

        public AutoMapperProfile()
        {
            CreateMap<Employee, GetEmployeeDto>();

            // When mapping the AddEmployeeDto to an Employee, inject the
            // default employee pay rate if a pay rate was not defined.
            CreateMap<AddEmployeeDto, Employee>()
                .ForMember(dest => dest.PayRate, opt => opt.NullSubstitute(_defaultPayRate))
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
