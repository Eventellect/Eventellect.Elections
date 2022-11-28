using Elections.Domain.Interfaces;
using Elections.Domain.Interfaces.Repo;

namespace Elections.Infrastructure
{

    public class CandidateRepo : ICandidateRepo
    {
        private IReadOnlyList<ICandidate> _official;
        public IReadOnlyList<ICandidate> Official => _official;

        public CandidateRepo()
        {
            _official = GetNewYorkCityDemocraticMayoralPrimary().ToList();
        }
    
        public IReadOnlyList<ICandidate> GetCandidates()
        {
            return Official;
        }
      
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
       
        private record Candidate(int Id, string Name) : ICandidate, IVoter;

    }

}
