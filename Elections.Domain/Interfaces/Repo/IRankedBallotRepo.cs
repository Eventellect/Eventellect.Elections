namespace Elections.Domain.Interfaces.Repo
{
    public interface IRankedBallotRepo
    {
        IReadOnlyList<IRankedBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates);
    }
}
