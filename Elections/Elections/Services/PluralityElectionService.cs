﻿using Elections.Ballots.Interfaces;
using Elections.Elections.Exceptions;
using Elections.Elections.Interfaces;
using Elections.Voters.Interfaces;

namespace Elections.Elections.Services;

public class PluralityElectionService : IElection<ISingleVoteBallot>
{
    public Task<ICandidate> Run(IReadOnlyList<ISingleVoteBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
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

        var winner = candidateRank.FirstOrDefault();
        if (winner == null)
        {
            throw new NoWinnerException();
        }

        var tiedWinners = candidateRank.Where(x => x.VoteCount == winner.VoteCount);
        if (tiedWinners.Count() > 1)
        {
            throw new WinnerTiedException(tiedWinners.Select(x => x.Candidate).ToList(), winner.VoteCount);
        }

        return Task.FromResult(winner.Candidate);
    }
}