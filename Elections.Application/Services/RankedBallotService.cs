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
    public class RankedBallotService : IRankedBallotService
    {
        private IRankedBallotRepo _rankedBallotRepository;
        
        public RankedBallotService(IRankedBallotRepo rankedBallotRepository)
        {
            _rankedBallotRepository = rankedBallotRepository;
        }
        
       
        public IReadOnlyList<IRankedBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
        {
            return _rankedBallotRepository.Create(voters, candidates);
        }
       
    }

}
