using Elections;
using Elections.Interfaces;
using Elections.Ballots;
using Elections.Elections;
using System.Diagnostics;
using Elections.Elections.Exceptions;

const int numVoters = 6;
var voters = Voters.Create(numVoters, Candidates.Official);

RunSimpleElection(voters);

RunRankedChoiceElection(voters);

static void RunSimpleElection(IReadOnlyList<IVoter> voters)
{
    var ballots = SingleVoteBallotFactory.Create(voters, Candidates.Official);
    RunElection<PluralityElection, ISingleVoteBallot>(ballots);
}

static void RunRankedChoiceElection(IReadOnlyList<IVoter> voters)
{
    var ballots = RankedBallotFactory.Create(voters, Candidates.Official);
    RunElection<RankedChoiceElection, IRankedBallot>(ballots);
}

static void RunElection<TElection, TBallot>(IReadOnlyList<TBallot> ballots)
    where TElection : IElection<TBallot>, new()
    where TBallot : IBallot
{
    var stopwatch = Stopwatch.StartNew();
    Console.WriteLine($"========== {typeof(TElection).Name} ==========");
    Console.WriteLine();

    try
    {
        var election = new TElection();
        var winner = election.Run(ballots, Candidates.Official);
        Console.WriteLine(FormatMessage($"Winner is {winner?.Name}"));
    }
    catch (WinnerTiedException ex)
    {
        Console.WriteLine($"Error: the following winners were tied with {ex.TiedVotes} votes:");
        ex.TiedWinners.Select(x => x.Name).ToList().ForEach(Console.WriteLine);
    }
    catch (NoMajorityException)
    {
        Console.WriteLine("Error: No majority could be determined");
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