using Elections.Domain.Interfaces;

namespace Elections.Infrastructure
{
    public class RandomHelper : IRandomHelper
    {
        private const int _writeInFactor = 1337;
        private static readonly IReadOnlyList<ICandidate> _writeIns = GetSupremeCourtJustices().ToList();

        public ICandidate SelectRandom(IReadOnlyList<ICandidate> candidates)
        {
            var candidatePool = UseWriteIn() ? _writeIns : candidates;
            return candidatePool[Random.Shared.Next(candidatePool.Count)];
        }

        private bool UseWriteIn()
        {
            var randomNumber = Random.Shared.Next();
            return randomNumber % _writeInFactor == 0;
        }

        private static IEnumerable<ICandidate> GetSupremeCourtJustices()
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

        private record Candidate(int Id, string Name) : ICandidate, IVoter;
    }
}
