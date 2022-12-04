using Elections.Voters.Interfaces;
using Elections.Voters.Records;

namespace Elections.Voters.Services;

public class VoterDataService : IVoterDataService
{
    public IReadOnlyList<IVoter> Create(int voterCount, IReadOnlyList<ICandidate> candidates)
    {
        var maxCandidateId = candidates.Max(c => c.Id);
        var voters = Enumerable.Range(maxCandidateId + 1, voterCount).Select(CreateVoter).ToList();
        voters.AddRange(candidates);
        return voters;
    }

    private IVoter CreateVoter(int id)
    {
        return new Voter(id, $"Voter {id}");
    }
}
