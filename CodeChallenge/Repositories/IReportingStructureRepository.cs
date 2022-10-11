using System;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public interface IReportingStructureRepository
    {
        ReportingStructure GetReportingStructure(string id);
    }
}

