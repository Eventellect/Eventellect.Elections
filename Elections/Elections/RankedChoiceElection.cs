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
            /*
             * Notes:
             *  - https://fairvote.org/our-reforms/ranked-choice-voting/
             *  - Anyone with over 50% wins;
             *  - If no winner, then drop the lowest candidate and re-count;
             *  - Repeat until a winner is found;
             */
            var candidatesInRound = new Dictionary<ICandidate, int>( );
            var candidatesRemoved = new List<ICandidate>();
            var totalBallots = ballots.Count;
            for (int round = 0; round <= 10; round++)
            {
                foreach (var ballot in ballots)
                {
                    if (ballot.Votes.Count > round)
                    {
                        var vote = ballot.Votes[round];
                        if (!candidatesRemoved.Contains(vote.Candidate))
                        {
                            candidatesInRound.TryGetValue(
                            key: vote.Candidate,
                            value: out var numberOfVotes
                        );
                            candidatesInRound[vote.Candidate] = numberOfVotes + 1;
                        }
                    }
                }
                var winner =
                    candidatesInRound
                        .OrderByDescending(x => x.Value)
                        .FirstOrDefault();
                if (winner.Value > (totalBallots / 2))
                {
                    return winner.Key;
                }
                var lowestRanked =
                    candidatesInRound
                        .OrderBy(x => x.Value)
                        .FirstOrDefault();
                candidatesInRound.Remove(lowestRanked.Key);
                candidatesRemoved.Add(lowestRanked.Key);
            }

            // Note: In case a winner is not found (although highly unlikely).
            return null;
        }
        else
        {
            throw new Exception("There are no Ballots and/or Candidates.");
        }
    }
}