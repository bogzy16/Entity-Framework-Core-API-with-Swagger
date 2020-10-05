using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Data;
using REST_API.Repository;
using Microsoft.Extensions.Logging;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IDataRepository<EmployeeRecord> _dataRepository;

        public EmployeeController(ILogger<EmployeeRecord> logger, IDataRepository<EmployeeRecord> dataRepository)
        {
            _logger = logger;
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public async Task<ActionResult> ViewList()
        {
            _logger?.LogDebug("'{0}' has been invoked", nameof(ViewList));

            try
            {
                IEnumerable<EmployeeRecord> employees = await _dataRepository.GetAllEmployee();

                _logger?.LogInformation("Records have been retrieved successfully.");

                return Ok(employees);
            }
            catch(Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(ViewList), ex);
                return null;
            }
        }

        [HttpPost, Route("Filter")]
        public async Task<List<EmployeeRecord>> GetFilteredRecord(Request req)
        {
            _logger?.LogDebug("'{0}' has been invoked", nameof(GetFilteredRecord));

            try
            {
                List<EmployeeRecord> employees = await _dataRepository.GetFilteredList(req);

                _logger?.LogInformation("Filtered Records have been retrieved successfully.");

                return employees;
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetFilteredRecord), ex);
                return null;
            }
        }

        [HttpPost, Route("AddRecord")]
        public async Task<EmployeeRecord> AddRecord([FromBody] Request req)
        {
            _logger?.LogDebug("'{0}' has been invoked", nameof(AddRecord));

            try
            {
                var response = await _dataRepository.NewEmployee(req);

                _logger?.LogInformation("Record successfully added.");

                return response;
            }
            catch(Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(AddRecord), ex);
                return null;
            }
            
        }

        [HttpPut]
        public async Task<EmployeeRecord> ModifyRecord([FromBody] Request req)
        {
            _logger?.LogDebug("'{0}' has been invoked", nameof(ModifyRecord));

            try
            {
                var response = await _dataRepository.UpdateEmployee(req);

                _logger?.LogInformation("Record successfully modified.");

                return response;
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(ModifyRecord), ex);
                return null;
            }
        }

        [HttpDelete]
        public async Task<EmployeeRecord> Delete([FromBody] Request req)
        {

            _logger?.LogDebug("'{0}' has been invoked", nameof(Delete));

            try
            {
                var response = await _dataRepository.RemoveEmployee(req);

                _logger?.LogInformation("Record successfully deleted.");

                return response;
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(Delete), ex);
                return null;
            }
        }
    }
}