using Elections.Elections.Exceptions;
using Elections.Interfaces;

namespace Elections.Elections;

public class RankedChoiceElection : IElection<IRankedBallot>
{
    private int passNum = 1;

    public ICandidate Run(IReadOnlyList<IRankedBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        //Console.WriteLine($"Pass {passNum++} - {candidates.Count} candidates");

        if (!candidates.Any())
        {
            throw new NoMajorityException();
        }

        var totalVoters = ballots.Count;
        var voterMajority = (int)Math.Floor((decimal)totalVoters / 2) + 1;

        var voterTopSelections = ballots
            .Select(x =>
                new
                {
                    x.Voter,
                    Candidate = x.Votes
                        // Cross-reference candidates list since it controls which candidates are eligible for the calculation
                        .Join(candidates, vote => vote.Candidate, candidate => candidate, (vote, _) => vote)
                        .OrderBy(y => y.Rank)
                        .Select(y => y.Candidate)
                        .FirstOrDefault()
                })
            .Where(x => x.Candidate != null);

        var candidateRank = voterTopSelections
            .GroupBy(x => x.Candidate, (c, v) =>
                new
                {
                    Candidate = c!,
                    VoteCount = v.Count()
                })
            .OrderByDescending(x => x.VoteCount)
            .ToList();

        var highestRanked = candidateRank.First();
        var lowestRanked = candidateRank.Last();

        if (candidateRank.Count > 1 && highestRanked.VoteCount == lowestRanked.VoteCount)
        {
            throw new WinnerTiedException(
                candidateRank.Select(x => x.Candidate).ToList(),
                highestRanked.VoteCount);
        }

        if (highestRanked.VoteCount >= voterMajority)
        {
            return highestRanked.Candidate!;
        }

        return Run(
            ballots,
            candidates.Where(x => x.Id != lowestRanked.Candidate!.Id).ToList()
        );
    }
}