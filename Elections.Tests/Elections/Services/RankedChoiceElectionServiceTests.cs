using System.ComponentModel;
using Elections.Ballots.Interfaces;
using Elections.Ballots.Records;
using Elections.Elections;
using Elections.Elections.Exceptions;
using Elections.Voters.Interfaces;
using Elections.Voters.Records;

namespace Elections.Tests.Elections.Services;

[TestClass]
public class RankedChoiceElectionServiceTests
{
    private List<ICandidate> candidates; 

    [TestInitialize]
    public void Setup()
    {
        candidates = new List<ICandidate>
        {
            new Candidate(101, "Candidate1"),
            new Candidate(102, "Candidate2"),
            new Candidate(103, "Candidate3"),
            new Candidate(104, "Candidate4")
        };
    }

    [TestMethod]
    public async Task Run_WithTiedWinners_ThrowsWinnerTiedException()
    {
        var ballots = new List<IRankedBallot>
        {
            new RankedChoiceBallot(new Voter(1, "Voter1"), new List<IRankedVote> { new RankedChoiceVote(candidates[0], 1) }),
            new RankedChoiceBallot(new Voter(2, "Voter2"), new List<IRankedVote> { new RankedChoiceVote(candidates[1], 1) }),
        };

        var service = new RankedChoiceElectionService();

        await Assert.ThrowsExceptionAsync<WinnerTiedException>(async () =>
        {
            await service.Run(ballots, candidates);
        });
    }

    [TestMethod]
    public async Task Run_WithNoWinningMajority_ThrowsNoMajorityException()
    {
        var ballots = new List<IRankedBallot>
        {
            // Votes for Candidate 1
            new RankedChoiceBallot(new Voter(1, "Voter1"), new List<IRankedVote> { new RankedChoiceVote(candidates[0], 1) }),
            new RankedChoiceBallot(new Voter(2, "Voter2"), new List<IRankedVote> { new RankedChoiceVote(candidates[0], 1) }),

            // Vote for Candidate 2, 3, and 4
            new RankedChoiceBallot(new Voter(3, "Voter3"), new List<IRankedVote> { new RankedChoiceVote(candidates[1], 1) }),
            new RankedChoiceBallot(new Voter(4, "Voter4"), new List<IRankedVote> { new RankedChoiceVote(candidates[2], 1) }),
            new RankedChoiceBallot(new Voter(5, "Voter5"), new List<IRankedVote> { new RankedChoiceVote(candidates[3], 1) })
        };

        var service = new RankedChoiceElectionService();

        await Assert.ThrowsExceptionAsync<NoMajorityException>(async () =>
        {
            await service.Run(ballots, candidates);
        });
    }

    [TestMethod]
    public async Task Run_WithMajority_Succeeds()
    {
        var ballots = new List<IRankedBallot>
        {
            new RankedChoiceBallot(new Voter(1, "Voter1"), new List<IRankedVote> { new RankedChoiceVote(candidates[0], 1) }),
            new RankedChoiceBallot(new Voter(2, "Voter2"), new List<IRankedVote> { new RankedChoiceVote(candidates[0], 1) }),
            new RankedChoiceBallot(new Voter(3, "Voter3"), new List<IRankedVote> { new RankedChoiceVote(candidates[1], 1) }),
        };

        var service = new RankedChoiceElectionService();
        var winner = await service.Run(ballots, candidates);

        Assert.IsNotNull(winner);
        Assert.AreEqual(candidates[0], winner);
    }
}