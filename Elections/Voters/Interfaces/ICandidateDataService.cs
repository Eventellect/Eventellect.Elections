namespace Elections.Voters.Interfaces;

public interface ICandidateDataService
{
    IReadOnlyList<ICandidate> GetOfficial();
    ICandidate SelectRandom(IReadOnlyList<ICandidate> candidates);
}