using System.Collections.Generic;

namespace Elections.Domain.Interfaces;

public interface IElection<TBallot>
    where TBallot : IBallot
{
    ICandidate Run(IReadOnlyList<TBallot> ballots, IReadOnlyList<ICandidate> candidates);
}