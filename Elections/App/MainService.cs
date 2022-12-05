using System.Diagnostics;
using Elections.Ballots.Interfaces;
using Elections.Elections.Exceptions;
using Elections.Elections.Interfaces;
using Elections.Voters.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elections.App;

public class MainService
{
    private readonly Settings settings;
    private readonly ILogger<MainService> logger;
    private readonly ICandidateDataService candidateDataService;
    private readonly IVoterDataService voterDataService;
    private readonly IBallotFactory<IRankedBallot> rankedBallotFactory;
    private readonly IBallotFactory<ISingleVoteBallot> singleVoteBallotFactory;
    private readonly IElection<ISingleVoteBallot> pluralityElectionService;
    private readonly IElection<IRankedBallot> rankedChoiceElectionService;

    public MainService(
        IOptions<Settings> settings,
        ILogger<MainService> logger,
        ICandidateDataService candidateDataService,
        IVoterDataService voterDataService,
        IBallotFactory<ISingleVoteBallot> singleVoteBallotFactory,
        IBallotFactory<IRankedBallot> rankedBallotFactory,
        IElection<ISingleVoteBallot> pluralityElectionService,
        IElection<IRankedBallot> rankedChoiceElectionService)
    {
        this.settings = settings.Value;
        this.logger = logger;
        this.candidateDataService = candidateDataService;
        this.voterDataService = voterDataService;
        this.rankedBallotFactory = rankedBallotFactory;
        this.singleVoteBallotFactory = singleVoteBallotFactory;
        this.pluralityElectionService = pluralityElectionService;
        this.rankedChoiceElectionService = rankedChoiceElectionService;
    }

    public async Task RunElectionsAsync()
    {
        var voters = voterDataService.Create(settings.NumVoters, candidateDataService.GetOfficial());
        await RunElection(voters, singleVoteBallotFactory, pluralityElectionService);
        await RunElection(voters, rankedBallotFactory, rankedChoiceElectionService);


        // Or an example of running the elections simultaneously:
        // (NOTE: this code doesn't currently handle exceptions (such as WinnerTiedException) and unit tests don't support this scenario)

        //var voters = voterDataService.Create(settings.NumVoters, candidateDataService.GetOfficial());
        //var candidates = candidateDataService.GetOfficial();

        //var singleBallots = singleVoteBallotFactory.Create(voters, candidates);
        //var rankedBallots = rankedBallotFactory.Create(voters, candidates);

        //var tasks = new[]
        //{
        //    pluralityElectionService.Run(singleBallots, candidates),
        //    rankedChoiceElectionService.Run(rankedBallots, candidates)
        //};

        //await Task.WhenAll(tasks).ContinueWith(task =>
        //{
        //    task.Result.Select(x => $"Winner: {x.Name}").ToList().ForEach(Console.WriteLine);
        //});
    }

    private async Task RunElection<TBallot>(IReadOnlyList<IVoter> voters, IBallotFactory<TBallot> ballotFactory, IElection<TBallot> election)
        where TBallot : IBallot
    {
        var officialCandidates = candidateDataService.GetOfficial();
        var ballots = ballotFactory.Create(voters, officialCandidates);

        var stopwatch = Stopwatch.StartNew();
        logger.LogInformation($"== Starting {election.GetType().Name}");

        try
        {
            var candidates = candidateDataService.GetOfficial();
            var winner = await election.Run(ballots, candidates);
            logger.LogInformation(FormatMessage($"Winner is {winner?.Name}"));
        }
        catch (WinnerTiedException ex)
        {
            var tiedWinners = string.Join(", ", ex.TiedWinners.Select(x => x.Name).ToList());
            logger.LogError($"The following winners were tied with {ex.TiedVotes} votes: {tiedWinners}");
        }
        catch (NoMajorityException)
        {
            logger.LogError("No majority could be determined");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unknown error occurred");
        }

        logger.LogInformation($"== Completed {election.GetType().Name}");

        string FormatMessage(string prefix)
            => $"{prefix} [{stopwatch!.Elapsed.TotalMilliseconds} ms]";
    }
}
