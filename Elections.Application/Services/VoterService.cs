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
    public class VoterService : IVoterService
    {
        private readonly IVoterRepo _voterRepo;

        public VoterService(IVoterRepo voterRepo)
        {
            _voterRepo = voterRepo;
        }

        public IReadOnlyList<IVoter> CreateVoters(int voterCount, IReadOnlyList<ICandidate> candidates)
        {
            return _voterRepo.CreateVoters(voterCount, candidates);
        }
        
    }
}
