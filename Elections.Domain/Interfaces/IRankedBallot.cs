namespace Elections.Domain.Interfaces;

public interface IRankedBallot : IBallot
{
    IReadOnlyList<IRankedVote> Votes { get; }
}
