namespace Elections.Domain.Interfaces;

public interface IBallot
{
    IVoter Voter { get; }
}
