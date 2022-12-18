using Elections.Interfaces;

namespace Elections.Elections;

public class PluralityElection : IElection<ISingleVoteBallot>
{
    public ICandidate? Run(IReadOnlyList<ISingleVoteBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        if (ballots == null || ballots.Count <= 0)
        {
            return null;
        }

        var candidateCount = ballots.GroupBy(ballot => ballot.Vote.Candidate).Select((candidate) => new { Candidate = candidate.Key, Count = candidate.Count() });
        return candidateCount?.OrderByDescending(candidate => candidate.Count).FirstOrDefault()?.Candidate;
    }
}
