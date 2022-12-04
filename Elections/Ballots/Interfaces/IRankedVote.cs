namespace Elections.Ballots.Interfaces;

public interface IRankedVote : IVote
{
    int Rank { get; }
}
