namespace Elections.Voters.Interfaces;

public interface IVoterDataService
{
    IReadOnlyList<IVoter> Create(int voterCount, IReadOnlyList<ICandidate> candidates);
}