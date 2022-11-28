using Elections.Domain.Entities;
using Elections.Domain.Interfaces;
using Elections.Domain.Interfaces.Repo;

namespace Elections.Infrastructure
{
    public class SingleVoteBallotRepo : ISingleVoteBallotRepo
    {
        private IRandomHelper _randomHelper;

        public SingleVoteBallotRepo(IRandomHelper randomHelper)
        {
            _randomHelper = randomHelper;
        }

        public IReadOnlyList<ISingleVoteBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
        {
            return voters.Select(x => CreateSimpleBallot(x, candidates)).ToList();
        }

        private SimpleBallot CreateSimpleBallot(IVoter voter, IReadOnlyList<ICandidate> candidates)
        {
            var vote = CreateSimpleVote(voter, candidates);
            return new SimpleBallot(voter, vote);
        }

        private SimpleVote CreateSimpleVote(IVoter voter, IReadOnlyList<ICandidate> candidates)
        {
            if (voter is ICandidate candidate)
                return new SimpleVote(candidate);

            var voterCandidate = _randomHelper.SelectRandom(candidates);
            return new SimpleVote(voterCandidate);
        }

    }
}
