namespace Elections.Domain.Interfaces;

public interface IVote
{
    ICandidate Candidate { get; }
}
