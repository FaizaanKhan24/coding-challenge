using System;
using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetCompensation(string id);
        Compensation AddCompensation(Compensation compensation);
        Task SaveAsync();
    }
}

