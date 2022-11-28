using Elections.Domain.Entities;
using Elections.Domain.Interfaces;
using Elections.Domain.Interfaces.Repo;

namespace Elections.Infrastructure
{
    public class RankedBallotRepo : IRankedBallotRepo
    {
        private IRandomHelper _randomHelper;

        public RankedBallotRepo(IRandomHelper randomHelper)
        {
            _randomHelper = randomHelper;
        }

        public IReadOnlyList<IRankedBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
        {
            return voters.Select(x => CreateBallot(x, candidates)).ToList();
        }

        private RankedChoiceBallot CreateBallot(IVoter voter, IReadOnlyList<ICandidate> candidates)
        {
            if (voter is ICandidate candidate)
                return CreateCandidateBallot(voter, candidate);

            return CreateVoterBallot(voter, candidates);
        }

        private RankedChoiceBallot CreateCandidateBallot(IVoter voter, ICandidate candidate)
        {
            return new RankedChoiceBallot(voter, new[] { new RankedChoiceVote(candidate, 1) });
        }

        private RankedChoiceBallot CreateVoterBallot(IVoter voter, IReadOnlyList<ICandidate> candidates)
        {
            var candidateCount = GetCandidateCount(candidates);
            var availableCandidates = candidates.ToList();
            var votes = new List<RankedChoiceVote>();

            for (int i = 0; i < candidateCount; i++)
            {
                var voteCandidate = _randomHelper.SelectRandom(availableCandidates);
                availableCandidates.Remove(voteCandidate);
                votes.Add(new RankedChoiceVote(voteCandidate, i + 1));
            }

            return new RankedChoiceBallot(voter, votes);
        }

        private int GetCandidateCount(IReadOnlyList<ICandidate> candidates)
        {
            return Random.Shared.Next() % candidates.Count + 1;
        }
    }
}
