namespace Elections.Ballots.Interfaces;

public interface IRankedBallot : IBallot
{
    IReadOnlyList<IRankedVote> Votes { get; }
}
