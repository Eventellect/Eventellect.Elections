using Elections.Interfaces;

namespace Elections.Elections;

public class RankedChoiceElection : IElection<IRankedBallot>
{
    public ICandidate Run(IReadOnlyList<IRankedBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        // collect all ballots and order them by chosen rank, and candidate
        var allBallots = ballots.Select(x => x.Votes.OrderBy(y => y.Rank).Select(y => y.Candidate).ToList()).ToList(); 
        // Use a localized list to remove candidates on the Ranked Choice Election process
        List<ICandidate> modifiableCandidateList = (List<ICandidate>)candidates;

        while (modifiableCandidateList.Count() > 1)
        {
            // While we have more than one candidate...
            // Select the current candidate list
                // Select all ballots that have a score within the modifiableCandidateList and join to create records that can sort in decending order based upon candidate
            var votes =
                from mcl in modifiableCandidateList
                join ab in allBallots.Select(x => x.Where(y => modifiableCandidateList.Contains(y)).FirstOrDefault()).Where(x => x != null) on mcl equals ab into mclab
                select new { candidate = mcl, votes = mclab.Count() };

            var candidateRank = votes.OrderByDescending(x => x.votes)
                                 .GroupBy(x => x.votes, x => x.candidate)
                                 .ToList();

            // remove candidates in last position based upon candidate sort     
            var candidateToRemove = modifiableCandidateList.Where(c => c.Id == candidateRank.Last().Last().Id).First();
            modifiableCandidateList.Remove(candidateToRemove);
        }

        //return final candidate not eliminated

        return modifiableCandidateList.First();
    }
}
