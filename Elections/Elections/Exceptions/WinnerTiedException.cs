using Elections.Voters.Interfaces;

namespace Elections.Elections.Exceptions;

public class WinnerTiedException : Exception
{
    public IReadOnlyList<ICandidate> TiedWinners { get; }

    public int TiedVotes { get; }

    public WinnerTiedException(IReadOnlyList<ICandidate> tiedWinners, int tiedVotes)
    {
        TiedWinners = tiedWinners;
        TiedVotes = tiedVotes;
    }
}