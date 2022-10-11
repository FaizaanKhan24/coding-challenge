using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _compensationService = compensationService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "getCompensation")]
        public IActionResult GetCompensation(string id)
        {
            _logger.LogDebug($"Request for [{id}] compensation");

            var compensation = _compensationService.GetCompensation(id);

            if(compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody]Compensation compensation)
        {
            _logger.LogDebug($"Request to add new compensation for {compensation.Employee.FirstName} {compensation.Employee.LastName} recieved");

            _compensationService.CreateCompensation(compensation);

            return CreatedAtRoute("getCompensation", new { id = compensation.Employee.EmployeeId }, compensation);
        }
    }
}

