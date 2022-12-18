using Elections.Interfaces;

namespace Elections.Elections;

public class RankedChoiceElection : IElection<IRankedBallot>
{
    public ICandidate? Run(IReadOnlyList<IRankedBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        if (ballots == null || ballots.Count <= 0 || candidates == null || candidates.Count <= 0)
        {
            return null;
        }

        var preferenceStep = 0;
        var candidatesDic = new Dictionary<ICandidate, int>();
        var candidatesEliminated = new List<ICandidate>();

        while (preferenceStep <= candidates.Count)
        {
            foreach (var ballot in ballots)
            {
                if (ballot.Votes.Count > preferenceStep)
                {
                    var candidate = ballot.Votes[preferenceStep].Candidate;
                    candidatesDic.TryGetValue(candidate, out int count);
                    candidatesDic[candidate] = count + 1;
                }
            }

            foreach (var eliminated in candidatesEliminated)
            {
                candidatesDic.Remove(eliminated);
            }

            var highestVotes = candidatesDic.OrderByDescending(candidate => candidate.Value).FirstOrDefault();

            if (highestVotes.Value > ballots.Count / 2)
            {
                return highestVotes.Key;
            }
            else
            {
                var eliminated = candidatesDic.OrderBy(candidate => candidate.Value).FirstOrDefault();
                candidatesDic.Remove(eliminated.Key);
                candidatesEliminated.Add(eliminated.Key);
            }

            preferenceStep++;
        }

        return null;
    }
}