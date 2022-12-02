using Elections.Elections.Exceptions;
using Elections.Interfaces;

namespace Elections.Elections;

public class PluralityElection : IElection<ISingleVoteBallot>
{
    public ICandidate Run(IReadOnlyList<ISingleVoteBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        //ballots.Select(x => $"{x.Voter.Name} - {x.Vote.Candidate.Name}").ToList().ForEach(Console.WriteLine);

        var candidateRank = ballots
            .GroupBy(x => x.Vote.Candidate, (c, v) => new
            {
                Candidate = c,
                VoteCount = v.Count()
            })
            // Cross-reference candidates list since it controls which candidates are eligible for the calculation
            // (This probably isn't necessary for plurality voting)
            .Join(candidates, vote => vote.Candidate, candidate => candidate, (vote, _) => vote)
            .OrderByDescending(x => x.VoteCount);

        //candidateRank.Select(x => $"{x.Candidate.Name} - {x.VoteCount}").ToList().ForEach(Console.WriteLine);

        var winner = candidateRank.FirstOrDefault();
        if (winner == null)
        {
            throw new ArgumentNullException(nameof(winner));
        }

        var tiedWinners = candidateRank.Where(x => x.VoteCount == winner.VoteCount);
        if (tiedWinners.Count() > 1)
        {
            throw new WinnerTiedException(tiedWinners.Select(x => x.Candidate).ToList(), winner.VoteCount);
        }

        return winner.Candidate;
    }
}
