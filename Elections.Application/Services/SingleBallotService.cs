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
    public class SingleBallotService : ISingleBallotService
    {
        private ISingleVoteBallotRepo _singleVoteBallotRepository;
        
        public SingleBallotService(ISingleVoteBallotRepo singleVoteBallotRepository)
        {
            _singleVoteBallotRepository = singleVoteBallotRepository;
        }

        
        public IReadOnlyList<ISingleVoteBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
        {
            return _singleVoteBallotRepository.Create(voters, candidates);
        }
       
    }
}
