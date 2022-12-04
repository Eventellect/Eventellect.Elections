namespace Elections.Ballots.Interfaces;

public interface ISingleVoteBallot : IBallot
{
    IVote Vote { get; }
}
