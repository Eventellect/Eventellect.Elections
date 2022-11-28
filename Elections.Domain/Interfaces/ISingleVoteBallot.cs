namespace Elections.Domain.Interfaces;

public interface ISingleVoteBallot : IBallot
{
    IVote Vote { get; }
}
