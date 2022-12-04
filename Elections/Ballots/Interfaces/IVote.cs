using Elections.Voters.Interfaces;

namespace Elections.Ballots.Interfaces;

public interface IVote
{
    ICandidate Candidate { get; }
}
