using Elections.Voters.Interfaces;

namespace Elections.Elections.Exceptions;

public class WinnerTiedException : Exception
{
    public IReadOnlyList<ICandidate> TiedWinners { get; }

    public int TiedVotes { get; }

    public WinnerTiedException() : this(new List<ICandidate>(), -1) { }

    public WinnerTiedException(IReadOnlyList<ICandidate> tiedWinners, int tiedVotes)
    {
        TiedWinners = tiedWinners;
        TiedVotes = tiedVotes;
    }
}