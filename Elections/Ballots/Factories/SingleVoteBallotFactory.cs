using Elections.Ballots.Interfaces;
using Elections.Ballots.Records;
using Elections.Voters.Interfaces;

namespace Elections.Ballots.Factories;

public class SingleVoteBallotFactory : IBallotFactory<ISingleVoteBallot>
{
    private readonly ICandidateDataService candidateDataService;

    public SingleVoteBallotFactory(ICandidateDataService candidateDataService)
    {
        this.candidateDataService = candidateDataService;
    }

    public IReadOnlyList<ISingleVoteBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
    {
        return voters.Select(x => CreateSimpleBallot(x, candidates)).ToList();
    }

    private SimpleBallot CreateSimpleBallot(IVoter voter, IReadOnlyList<ICandidate> candidates)
    {
        var vote = CreateSimpleVote(voter, candidates);
        return new SimpleBallot(voter, vote);
    }

    private SimpleVote CreateSimpleVote(IVoter voter, IReadOnlyList<ICandidate> candidates)
    {
        if (voter is ICandidate candidate)
            return new SimpleVote(candidate);

        var voterCandidate = candidateDataService.SelectRandom(candidates);
        return new SimpleVote(voterCandidate);
    }
}