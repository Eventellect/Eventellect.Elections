using Elections.Application.Election;
using Elections.Application.Interfaces.Services;
using Elections.Domain.Interfaces;
using Elections.Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

Console.WriteLine("Starting: Elections.UI.Console");
Console.WriteLine();

var IoCContainer = new DependencyContainer();
var builder = IoCContainer.RegisterServices(new ServiceCollection());

var voterService = builder.GetRequiredService<IVoterService>();
var candidateService = builder.GetRequiredService<ICandidateService>();
var singleBallotService = builder.GetRequiredService<ISingleBallotService>();
var rankedBallotService = builder.GetRequiredService<IRankedBallotService>();

const int numVoters = 100_000;

var candidates = candidateService.GetCandidates();
var voters = voterService.CreateVoters(numVoters, candidates);

RunSimpleElection(voters, candidates);
RunRankedChoiceElection(voters, candidates);


//TODO: Convert to Generic class
void RunSimpleElection(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
{
    var ballots = singleBallotService.Create(voters, candidates);
    RunElection<PluralityElection, ISingleVoteBallot>(ballots);
}

void RunRankedChoiceElection(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
{
    var ballots = rankedBallotService.Create(voters, candidates);
    RunElection<RankedChoiceElection, IRankedBallot>(ballots);
}

void RunElection<TElection, TBallot>(IReadOnlyList<TBallot> ballots)
    where TElection : IElection<TBallot>, new()
    where TBallot : IBallot
{
    var stopwatch = Stopwatch.StartNew();
    Console.WriteLine($"========== {typeof(TElection).Name} ==========");
    Console.WriteLine();

    try
    {
        var election = new TElection();
        var winner = election.Run(ballots, candidates);
        Console.WriteLine();
        Console.WriteLine(FormatMessage($"Winner is {winner?.Name}"));
    }
    catch (Exception ex)
    {
        Console.WriteLine(FormatMessage(ex.ToString()));
    }

    Console.WriteLine();
    Console.WriteLine($"============================================");
    Console.WriteLine();
    Console.WriteLine();

    string FormatMessage(string prefix)
        => $"{prefix} [{stopwatch!.Elapsed.TotalMilliseconds} ms]";
}