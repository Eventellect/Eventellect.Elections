using Elections.Interfaces;

namespace Elections.Elections;

public class PluralityElection : IElection<ISingleVoteBallot>
{
    public ICandidate Run(IReadOnlyList<ISingleVoteBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        Dictionary<ICandidate, int> voteTally = new Dictionary<ICandidate, int>();

        foreach (var b in ballots)
        {
            int currentCount = voteTally.Where(v => v.Key == b.Vote.Candidate).SingleOrDefault().Value;

            if (currentCount > 0)
            {
                voteTally.Remove(b.Vote.Candidate);
                voteTally.Add(b.Vote.Candidate, currentCount + 1);
            }
            else
                voteTally.Add(b.Vote.Candidate, currentCount + 1);
        }

        return voteTally.MaxBy(v => v.Value).Key;
    }
}
