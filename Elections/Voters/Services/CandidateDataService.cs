using Elections.App;
using Elections.Voters.Interfaces;
using Elections.Voters.Records;
using Microsoft.Extensions.Options;

namespace Elections.Voters.Services;

public class CandidateDataService : ICandidateDataService
{
    private readonly int writeInFactor;

    private IReadOnlyList<ICandidate>? official;
    private IReadOnlyList<ICandidate>? writeIns;

    public CandidateDataService(IOptions<Settings> config)
    {
        writeInFactor = config.Value.WriteInFactor;
    }

    public IReadOnlyList<ICandidate> GetOfficial()
        => official ??= GetNewYorkCityDemocraticMayoralPrimary().ToList();

    public ICandidate SelectRandom(IReadOnlyList<ICandidate> candidates)
    {
        var candidatePool = UseWriteIn() ? GetWriteIns() : candidates;
        return candidatePool[Random.Shared.Next(candidatePool.Count)];
    }

    private IReadOnlyList<ICandidate> GetWriteIns() 
        => writeIns ??= GetSupremeCourtJustices().ToList();

    private IEnumerable<ICandidate> GetNewYorkCityDemocraticMayoralPrimary()
    {
        yield return new Candidate(10001, "Eric Adams");
        yield return new Candidate(10002, "Shaun Donovan");
        yield return new Candidate(10003, "Kathryn Garcia");
        yield return new Candidate(10004, "Raymond McGuire");
        yield return new Candidate(10005, "Dianne Morales");
        yield return new Candidate(10006, "Scott Stringer");
        yield return new Candidate(10007, "Maya Wiley");
        yield return new Candidate(10008, "Andrew Yang");
    }

    private IEnumerable<ICandidate> GetSupremeCourtJustices()
    {
        yield return new Candidate(int.MaxValue - 1, "John G. Roberts, Jr.");
        yield return new Candidate(int.MaxValue - 2, "Clarence Thomas");
        yield return new Candidate(int.MaxValue - 3, "Samuel A. Alito, Jr.");
        yield return new Candidate(int.MaxValue - 4, "Sonia Sotomayor");
        yield return new Candidate(int.MaxValue - 5, "Elena Kagan");
        yield return new Candidate(int.MaxValue - 6, "Neil M. Gorsuch");
        yield return new Candidate(int.MaxValue - 7, "Brett M. Kavanaugh");
        yield return new Candidate(int.MaxValue - 8, "Amy Coney Barrett");
        yield return new Candidate(int.MaxValue - 9, "Ketanji Brown Jackson");
    }

    private bool UseWriteIn()
    {
        var randomNumber = Random.Shared.Next();
        return randomNumber % writeInFactor == 0;
    }
}
