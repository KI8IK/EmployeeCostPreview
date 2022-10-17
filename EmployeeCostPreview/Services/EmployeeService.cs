using AutoMapper;
using EmployeeCostPreview.Data;
using EmployeeCostPreview.Dtos.Employee;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCostPreview.Services
{
    /// <summary>
    /// CRUD services for Employee records
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EmployeeService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Add a new employee to the repository
        /// </summary>
        /// <param name="newEmployee">Employee DTO</param>
        /// <returns>List of all employee records</returns>
        async Task<ServiceResponse<List<GetEmployeeDto>>> IEmployeeService.AddEmployee(AddEmployeeDto newEmployee)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            try
            {
                var employee = _mapper.Map<Employee>(newEmployee);
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Employees
                     .Include(e => e.Dependents)
                     .Select(e => _mapper.Map<GetEmployeeDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Database save error. New employee not saved.";
                    serviceResponse.Error = $"{ex.GetType()} - {ex.Message}";
                }
                else
                    throw;
            }
            return serviceResponse;
        }

        /// <summary>
        /// Retrieve all employee records from the repository
        /// </summary>
        /// <returns>Collection of all employee records</returns>
        async Task<ServiceResponse<List<GetEmployeeDto>>> IEmployeeService.GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            var dbEmployees = await _context.Employees
                 .Include(e => e.Dependents)
                 .ToListAsync();
            serviceResponse.Data = dbEmployees.Select(e => _mapper.Map<GetEmployeeDto>(e)).ToList();
            return serviceResponse;
        }

        /// <summary>
        /// Retrieve a specific employee from the repository
        /// </summary>
        /// <param name="id">Primary key of the employee record</param>
        /// <returns>Request employee record, if it exists</returns>
        async Task<ServiceResponse<GetEmployeeDto>> IEmployeeService.GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var dbEmployee = await _context.Employees
                    .Include(e => e.Dependents)
                    .FirstAsync(e => e.Id == id);

                serviceResponse.Data = _mapper.Map<GetEmployeeDto>(dbEmployee);
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Constants.S_Error_EmployeeNotFoundMessage;
                serviceResponse.Error = $"{ex.GetType()} - An employee with an id of '{id}' could not be found.";
            }
            return serviceResponse;
        }

        /// <summary>
        /// Update an existing repository employee record
        /// </summary>
        /// <param name="updateEmployee">Employee record with changes</param>
        /// <returns>The updated employee record</returns>
        async Task<ServiceResponse<GetEmployeeDto>> IEmployeeService.UpdateEmployee(UpdateEmployeeDto updateEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var dbEmployee = await _context.Employees
                     .Include(e => e.Dependents)
                     .FirstAsync(e => e.Id == updateEmployee.Id);

                _mapper.Map(updateEmployee, dbEmployee);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetEmployeeDto>(dbEmployee);
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Constants.S_Error_EmployeeNotFoundMessage;
                serviceResponse.Error = $"{ex.GetType()} - An employee with an id of '{updateEmployee.Id}' could not be found.";
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Database save error. Employee '{updateEmployee.Id}' not updated.";
                    serviceResponse.Error = $"{ex.GetType()} - {ex.Message}";
                }
                else
                    throw;
            }
            return serviceResponse;
        }

        /// <summary>
        /// Removes a specific employee record from the repository
        /// </summary>
        /// <param name="id">Primary key of the employee record</param>
        /// <returns>A list of the employee records remaining in the repository</returns>
        async Task<ServiceResponse<List<GetEmployeeDto>>> IEmployeeService.DeleteEmployee(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            try
            {
                var dbEmployee = await _context.Employees.FirstAsync(e => e.Id == id);
                _context.Employees.Remove(dbEmployee);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Employees.Select(e => _mapper.Map<GetEmployeeDto>(e)).ToList();
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Constants.S_Error_EmployeeNotFoundMessage;
                serviceResponse.Error = $"{ex.GetType()} - An employee with an id of '{id}' could not be found.";
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Database save error. Employee '{id}' not deleted.";
                    serviceResponse.Error = $"{ex.GetType()} - {ex.Message}";
                }
                else
                    throw;
            }
            return serviceResponse;
        }
    }
}
