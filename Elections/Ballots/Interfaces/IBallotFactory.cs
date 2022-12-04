using Elections.Voters.Interfaces;

namespace Elections.Ballots.Interfaces;

public interface IBallotFactory<out TBallot> where TBallot : IBallot
{
    public IReadOnlyList<TBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates);
}