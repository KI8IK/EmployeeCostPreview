using AutoMapper;
using EmployeeCostPreview.Data;
using EmployeeCostPreview.Dtos.Dependent;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCostPreview.Services
{
    /// <summary>
    /// CRUD services for Dependent records
    /// </summary>
    public class DependentService : IDependentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public DependentService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Add a new dependent to the repository
        /// </summary>
        /// <param name="newEmployee">Dependent DTO</param>
        /// <returns>List of all dependent records</returns>
        async Task<ServiceResponse<List<GetDependentDto>>> IDependentService.AddDependent(AddDependentDto newDependent)
        {
            var serviceResponse = new ServiceResponse<List<GetDependentDto>>();
            try
            {
                var dependent = _mapper.Map<Dependent>(newDependent);
                _context.Dependents.Add(dependent);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Dependents.Select(d => _mapper.Map<GetDependentDto>(d)).ToListAsync();
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Database save error. New dependent not saved.";
                    serviceResponse.Error = $"{ex.GetType()} - {ex.Message}";
                }
                else
                    throw;
            }
            return serviceResponse;
        }

        /// <summary>
        /// Retrieve all dependent records from the repository
        /// </summary>
        /// <returns>Collection of all dependent records</returns>
        async Task<ServiceResponse<List<GetDependentDto>>> IDependentService.GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetDependentDto>>();
            var dbDependents = await _context.Dependents.ToListAsync();
            serviceResponse.Data = dbDependents.Select(d => _mapper.Map<GetDependentDto>(d)).ToList();
            return serviceResponse;
        }

        /// <summary>
        /// Retrieve a specific dependent from the repository
        /// </summary>
        /// <param name="id">Primary key of the dependent record</param>
        /// <returns>Request dependent record, if it exists</returns>
        async Task<ServiceResponse<GetDependentDto>> IDependentService.GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetDependentDto>();
            try
            {
                var dbDependent = await _context.Dependents.FirstAsync(d=> d.Id == id);
                serviceResponse.Data = _mapper.Map<GetDependentDto>(dbDependent);
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Constants.S_Error_DependentNotFoundMessage;
                serviceResponse.Error = $"{ex.GetType()} - A dependent with an id of '{id}' could not be found.";
            }
            return serviceResponse;
        }

        /// <summary>
        /// Update an existing repository dependent record
        /// </summary>
        /// <param name="updateEmployee">Dependent record with changes</param>
        /// <returns>The updated dependent record</returns>
        async Task<ServiceResponse<GetDependentDto>> IDependentService.UpdateDependent(UpdateDependentDto updateDependent)
        {
            var serviceResponse = new ServiceResponse<GetDependentDto>();
            try
            {
                var dbDependent = await _context.Dependents.FirstAsync(d => d.Id == updateDependent.Id);
                _mapper.Map(updateDependent, dbDependent);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetDependentDto>(dbDependent);
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Constants.S_Error_DependentNotFoundMessage;
                serviceResponse.Error = $"{ex.GetType()} - A dependent with an id of '{updateDependent.Id}' could not be found.";
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Database save error. Dependent '{updateDependent.Id}' not updated.";
                    serviceResponse.Error = $"{ex.GetType()} - {ex.Message}";
                }
                else
                    throw;
            }
            return serviceResponse;
        }

        /// <summary>
        /// Removes a specific dependent record from the repository
        /// </summary>
        /// <param name="id">Primary key of the dependent record</param>
        /// <returns>A list of the dependent records remaining in the repository</returns>
        async Task<ServiceResponse<List<GetDependentDto>>> IDependentService.DeleteDependent(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetDependentDto>>();
            try
            {
                var dbDependent = await _context.Dependents.FirstAsync(d => d.Id == id);
                _context.Dependents.Remove(dbDependent);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Dependents.Select(d => _mapper.Map<GetDependentDto>(d)).ToList();
            }
            catch (InvalidOperationException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = Constants.S_Error_DependentNotFoundMessage;
                serviceResponse.Error = $"{ex.GetType()} - A dependent with an id of '{id}' could not be found.";
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException || ex is DbUpdateConcurrencyException)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Database save error. Dependent '{id}' not deleted.";
                    serviceResponse.Error = $"{ex.GetType()} - {ex.Message}";
                }
                else
                    throw;
            }
            return serviceResponse;
        }
    }
}
