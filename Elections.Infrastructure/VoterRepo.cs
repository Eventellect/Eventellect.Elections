using Elections.Domain.Interfaces;
using Elections.Domain.Interfaces.Repo;

namespace Elections.Infrastructure
{
    public class VoterRepo : IVoterRepo
    {
        public IReadOnlyList<IVoter> CreateVoters(int voterCount, IReadOnlyList<ICandidate> candidates)
        {
            var totalVoters = voterCount + candidates.Count;
            var maxCandidateId = candidates.Max(c => c.Id);
            var voters = Enumerable.Range(maxCandidateId + 1, voterCount).Select(CreateVoter).ToList();
            voters.AddRange(candidates);
            return voters;
        }

        private static IVoter CreateVoter(int id)
        {
            return new Voter(id, $"Voter {id}");
        }

        private record Voter(int Id, string Name) : IVoter;
              
    }
}
