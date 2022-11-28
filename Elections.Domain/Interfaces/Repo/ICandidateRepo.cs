namespace Elections.Domain.Interfaces.Repo
{
    public interface ICandidateRepo
    {
        IReadOnlyList<ICandidate> GetCandidates();
    }
}
