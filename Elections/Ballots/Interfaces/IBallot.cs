using Elections.Voters.Interfaces;

namespace Elections.Ballots.Interfaces;

public interface IBallot
{
    IVoter Voter { get; }
}
