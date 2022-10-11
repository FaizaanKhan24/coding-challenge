using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reporting")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _reportingStructureService = reportingStructureService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "getReportingStructure")]
        public IActionResult GetReportingStructure(string id)
        {
            _logger.LogDebug($"Got a request for {id}");

            var reportingStructure = _reportingStructureService.GetReportingStructure(id);

            if(reportingStructure == null)
            {
                return NotFound();
            }

            return Ok(reportingStructure);
        }
    }
}

