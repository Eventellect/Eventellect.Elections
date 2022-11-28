namespace Elections.Domain.Interfaces;

public interface IRankedVote : IVote
{
    int Rank { get; }
}
