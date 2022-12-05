using Elections.Ballots.Records;
using Elections.Elections.Exceptions;
using Elections.Elections.Services;
using Elections.Voters.Interfaces;
using Elections.Voters.Records;

namespace Elections.Tests.Elections.Services;

[TestClass]
public class PluralityElectionServiceTests
{
    private readonly List<ICandidate> candidates = new()
    {
        new Candidate(101, "Candidate1"),
        new Candidate(102, "Candidate2")
    };

    [TestMethod]
    public async Task Run_WithTiedWinners_ThrowsWinnerTiedException()
    {
        var ballots = new List<SimpleBallot>
        {
            new(new Voter(1, "Voter1"), new SimpleVote(candidates[0])),
            new(new Voter(2, "Voter2"), new SimpleVote(candidates[1]))
        };
        var service = new PluralityElectionService();

        // Act/Assert
        await Assert.ThrowsExceptionAsync<WinnerTiedException>(async () =>
        {
            await service.Run(ballots, candidates);
        });
    }

    [TestMethod]
    public async Task Run_WithNoWinner_ThrowsNoWinnerException()
    {
        var ballots = new List<SimpleBallot>();
        var service = new PluralityElectionService();

        // Act/Assert
        await Assert.ThrowsExceptionAsync<NoWinnerException>(async () =>
        {
            await service.Run(ballots, candidates);
        });
    }

    [TestMethod]
    public async Task Run_WithWinner_Succeeds()
    {
        var ballots = new List<SimpleBallot>
        {
            new(new Voter(1, "Voter1"), new SimpleVote(candidates[0]))
        };

        var service = new PluralityElectionService();

        // Act
        var winner = await service.Run(ballots, candidates);

        // Assertions
        Assert.IsNotNull(winner);
        Assert.AreEqual(candidates[0], winner);
    }
}
