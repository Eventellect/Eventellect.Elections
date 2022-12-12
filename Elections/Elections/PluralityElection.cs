using System.Collections;
using Elections.Interfaces;

namespace Elections.Elections;

public class PluralityElection : IElection<ISingleVoteBallot>
{
    public ICandidate Run(
        IReadOnlyList<ISingleVoteBallot> ballots,
        IReadOnlyList<ICandidate> candidates )
    {
        if ( ( ballots != null && ballots.Count > 0 )
            && ( candidates != null && candidates.Count > 0 ) )
        {
            // Note: Initial 'brute force' approximation of winner, but does not consider ties.
            var dictionary = new Dictionary<ICandidate, int>();
            foreach( var ballot in ballots)
            {
                dictionary.TryGetValue(
                    key: ballot.Vote.Candidate,
                    value: out var voteCount
                );
                dictionary[ballot.Vote.Candidate] = voteCount + 1;
            }
            return dictionary.OrderByDescending(x => x.Value).FirstOrDefault().Key;
        }
        else
        {
            throw new Exception("There are no Ballots and/or Candidates.");
        }
    }
}
