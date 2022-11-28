namespace Elections.Domain.Interfaces.Repo
{
    public interface IVoterRepo
    {
        IReadOnlyList<IVoter> CreateVoters(int voterCount, IReadOnlyList<ICandidate> candidates);
    }
}
