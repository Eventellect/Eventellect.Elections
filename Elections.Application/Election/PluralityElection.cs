using Elections.Domain.Interfaces;

namespace Elections.Application.Election
{
    public class PluralityElection : IElection<ISingleVoteBallot>
    {
        public ICandidate Run(IReadOnlyList<ISingleVoteBallot> ballots, IReadOnlyList<ICandidate> candidates)
        {
            /** Plurality election algorithm
                1. Each voter votes for one candidate.
                2. The candidate with the most votes wins.
            **/

            var results = ballots.GroupBy(i => i.Vote.Candidate).OrderByDescending(x => x.Count());
#if DEBUG
            foreach (var result in results)
            {
                Console.WriteLine($"Candidate: {result.Key.Name} Votes: {result.Count()}");
            }
#endif
            var candidateWithMostVotes = results.First();
            var winner = candidates.First(x => x.Name.Equals(candidateWithMostVotes.Key.Name));

            return winner;
        }
    }
}
