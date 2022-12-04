using Elections.Ballots.Interfaces;
using Elections.Voters.Interfaces;

namespace Elections.Elections.Interfaces;

public interface IElection<TBallot>
    where TBallot : IBallot
{
    Task<ICandidate> Run(IReadOnlyList<TBallot> ballots, IReadOnlyList<ICandidate> candidates);
}