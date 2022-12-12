using System.Collections;
using Elections.Interfaces;

namespace Elections.Elections;

public class RankedChoiceElection : IElection<IRankedBallot>
{
    public ICandidate Run(
        IReadOnlyList<IRankedBallot> ballots,
        IReadOnlyList<ICandidate> candidates)
    {
        if ((ballots != null && ballots.Count > 0)
            && (candidates != null && candidates.Count > 0))
        {
            // Note: Initial 'brute force' approximation of winner, but does not consider ties.
            var dictionary = new Dictionary<ICandidate, int>();
            foreach( var ballot in ballots)
            {
                foreach ( var vote in ballot.Votes )
                {
                    dictionary.TryGetValue(
                        key: vote.Candidate,
                        value: out var rankNumber
                    );
                    if (rankNumber == 0)
                    {
                        dictionary[vote.Candidate] = vote.Rank;
                    }
                    else
                    {
                        dictionary[vote.Candidate] =
                            dictionary[vote.Candidate] += vote.Rank;
                    }
                }
            }
            return dictionary.OrderBy(x => x.Value).FirstOrDefault().Key;
        }
        else
        {
            throw new Exception("There are no Ballots and/or Candidates.");
        }
    }
}