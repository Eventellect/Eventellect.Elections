namespace Elections.Domain.Interfaces.Repo
{
    public  interface ISingleVoteBallotRepo
    {
        IReadOnlyList<ISingleVoteBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates);
    }
}
