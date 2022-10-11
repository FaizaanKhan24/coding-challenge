using System;
using CodeChallenge.Data;
using CodeChallenge.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;

namespace CodeChallenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IReportingStructureRepository> _logger;

        public ReportingStructureRepository(ILogger<IReportingStructureRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public ReportingStructure GetReportingStructure(string id)
        {
            ReportingStructure reportingStructure = new ReportingStructure();
            reportingStructure.ReportingStructureId = _employeeContext.Employees.FirstOrDefault(x => x.EmployeeId == id).EmployeeId;
            reportingStructure.Employee = _employeeContext.Employees.SingleOrDefault(x => x.EmployeeId == id);
            reportingStructure.NumberOfReport = GetNumberOfReporters(id);
            return reportingStructure;
        }

        private int GetNumberOfReporters(string id)
        {
            Queue queue = new Queue();
            int numberOfReports = 0;
            var directReports = _employeeContext.Employees.Include(x => x.DirectReports)
                .FirstOrDefault(x => x.EmployeeId == id)
                .DirectReports.Select(x => x.EmployeeId).ToList();
            foreach (var employeeId in directReports)
            {
                queue.Enqueue(employeeId);
            }

            while (queue.Count > 0)
            {
                string employeeId = queue.Dequeue().ToString();
                numberOfReports++;
                var reporters = _employeeContext.Employees.Include(x => x.DirectReports)
                    .FirstOrDefault(x => x.EmployeeId == employeeId)
                    .DirectReports.Select(x => x.EmployeeId).ToList();
                foreach (var eId in reporters)
                {
                    queue.Enqueue(eId);
                }
            }

            return numberOfReports;
        }
    }
}

