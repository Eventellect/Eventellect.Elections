using Elections.Domain.Entities;
using Elections.Domain.Interfaces;

namespace Elections.Application.Election
{
    public class RankedChoiceElection : IElection<IRankedBallot>
    {
        public ICandidate Run(IReadOnlyList<IRankedBallot> ballots, IReadOnlyList<ICandidate> candidates)
        {
            /** Ranked-choice-voting algorithm        
                1.  Each voter ranks the candidates in order of preference.
                2.  If any candidate has a majority of first-preference votes, that candidate is the winner.
                3.  Otherwise, the candidate with the fewest first-preference votes is eliminated.
                4.  The ballots of the eliminated candidate are redistributed to the next choice of the voters.
                5.  If a candidate has more than 50% of the votes, they win.
                6.  If no candidate has more than 50% of the votes, go to step 3.
            **/

            bool boolWinnerDeclared = false;
            ICandidate? winner = null;

#if DEBUG
            int round = 1;
#endif

            //Order the votes by rank for each ballot in case some where inserted out of order.
            var listBallots = ballots.OrderBy(i => i.Votes.Select(x => x.Rank).Min()).ToList();

            var listCandidates = candidates.ToList();

            do
            {
#if DEBUG
                Console.WriteLine($"Round {round}");
#endif

                var results = listBallots.Select(i => i.Votes.First()).GroupBy(x => x.Candidate).OrderByDescending(x => x.Count()).ToList();
                var candidateWithMostVotes = results.First();
                var candidateWithLeastVotes = results.Last();

                //Some candidates are not in the candidates list
                winner = candidateWithMostVotes.Key;
                var loser = candidateWithLeastVotes.Key;

                //Check if winner has majority votes (over .50)
                double percentage = DeterminePercentage(listBallots.Count(), candidateWithMostVotes.Count());

                if (percentage > 0.5)
                {
                    boolWinnerDeclared = true;
                }
                else
                {
                    //If no majority winner, get the loser and remove them from the list of candidates
                    var newCandidates = listCandidates.Where(x => !x.Name.Equals(loser.Name)).ToList();

                    //Remove the ballots for the loser
                    var newBallots = listBallots.Where(x => !x.Votes.First().Candidate.Name.Equals(loser.Name)).ToList();
                    var loserBallots = listBallots.Where(x => x.Votes.First().Candidate.Name.Equals(loser.Name)).ToList();

                    //Redistribute the ballots of the loser
                    foreach (var ballot in loserBallots)
                    {
                        var newVotes = ballot.Votes.OrderBy(x => x.Rank).Skip(1).ToList();
                        var voter = ballot.Voter;

                        //Possible that user only submitted one Rank 1 vote and nothing else.
                        if (newVotes.Count() > 0)
                        {
                            newBallots.Add(new RankedChoiceBallot(voter, newVotes));
                        }
                    }

                    listBallots.Clear();
                    listBallots.AddRange(newBallots.ToList());

                    listCandidates.Clear();
                    listCandidates.AddRange(newCandidates.ToList());
                }

#if DEBUG
                round++;
#endif

            } while (!boolWinnerDeclared);

            return winner;
        }

        private double DeterminePercentage(int ballots, int votes)
        {
            double decCandidateWithMostVotes = Convert.ToDouble(votes);
            double decBallots = Convert.ToDouble(ballots);
            double percentage = decCandidateWithMostVotes / decBallots;
            return percentage;
        }

    }
}



