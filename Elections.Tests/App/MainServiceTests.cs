using Elections.App;
using Elections.Ballots.Interfaces;
using Elections.Elections.Exceptions;
using Elections.Elections.Interfaces;
using Elections.Tests.Extensions;
using Elections.Voters.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elections.Tests.App;

[TestClass]
public class MainServiceTests
{
    private MainService mainService = null;
    private Mock<ILogger<MainService>> loggerMock;
    private Mock<ICandidateDataService> candidateDataServiceMock;
    private Mock<IVoterDataService> voterDataServiceMock;
    private Mock<IBallotFactory<ISingleVoteBallot>> singleVoteBallotFactoryMock;
    private Mock<IBallotFactory<IRankedBallot>> rankedBallotFactoryMock;
    private Mock<IElection<ISingleVoteBallot>> pluralityElectionServiceMock;
    private Mock<IElection<IRankedBallot>> rankedChoiceElectionServiceMock;


    [TestInitialize]
    public void Setup()
    {
        var settings = Options.Create(new Settings { NumVoters = 1, WriteInFactor = 1 });
        loggerMock = new Mock<ILogger<MainService>>();
        candidateDataServiceMock = new Mock<ICandidateDataService>();
        voterDataServiceMock = new Mock<IVoterDataService>();
        singleVoteBallotFactoryMock = new Mock<IBallotFactory<ISingleVoteBallot>>();
        rankedBallotFactoryMock = new Mock<IBallotFactory<IRankedBallot>>();
        pluralityElectionServiceMock = new Mock<IElection<ISingleVoteBallot>>();
        rankedChoiceElectionServiceMock = new Mock<IElection<IRankedBallot>>();

        mainService = new MainService(settings, loggerMock.Object, candidateDataServiceMock.Object,
            voterDataServiceMock.Object, singleVoteBallotFactoryMock.Object, rankedBallotFactoryMock.Object,
            pluralityElectionServiceMock.Object, rankedChoiceElectionServiceMock.Object
        );
    }

    [TestMethod]
    public async Task RunElectionsAsync_WithTiedWinners_LogsErrors()
    {
        pluralityElectionServiceMock
            .Setup(x => x.Run(It.IsAny<IReadOnlyList<ISingleVoteBallot>>(),
                It.IsAny<IReadOnlyList<ICandidate>>())).Throws<WinnerTiedException>();

        rankedChoiceElectionServiceMock
            .Setup(x => x.Run(It.IsAny<IReadOnlyList<IRankedBallot>>(),
                It.IsAny<IReadOnlyList<ICandidate>>())).Throws<WinnerTiedException>();

        await mainService.RunElectionsAsync();

        loggerMock.VerifyLog(LogLevel.Information, Times.Exactly(4));
        loggerMock.VerifyLog(LogLevel.Error, Times.Exactly(2));
    }

    [TestMethod]
    public async Task RunElectionsAsync_WithWinnersAndNoErrors_LogsInformationOnly()
    {
        await mainService.RunElectionsAsync();

        loggerMock.VerifyLog(LogLevel.Information, Times.Exactly(6));
        loggerMock.VerifyLog(LogLevel.Error, Times.Never);
    }
}
