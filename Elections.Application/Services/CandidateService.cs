using Elections.Domain.Interfaces.Repo;
using Elections.Application.Interfaces.Services;
using Elections.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elections.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepo _candidateRepository;
       
        public CandidateService(ICandidateRepo candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public IReadOnlyList<ICandidate> GetCandidates()
        {
           return _candidateRepository.GetCandidates();
        }
        
    }
}
