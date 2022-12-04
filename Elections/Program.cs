using Elections.App;
using Elections.Ballots.Factories;
using Elections.Ballots.Interfaces;
using Elections.Elections;
using Elections.Elections.Interfaces;
using Elections.Elections.Services;
using Elections.Voters.Interfaces;
using Elections.Voters.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((hostContext, services) =>
    services
        .AddScoped<MainService>()
        .AddScoped<ICandidateDataService, CandidateDataService>()
        .AddScoped<IVoterDataService, VoterDataService>()
        .AddScoped<IBallotFactory<ISingleVoteBallot>, SingleVoteBallotFactory>()
        .AddScoped<IBallotFactory<IRankedBallot>, RankedBallotFactory>()
        .AddScoped<IElection<ISingleVoteBallot>, PluralityElectionService>()
        .AddScoped<IElection<IRankedBallot>, RankedChoiceElectionService>()

        .AddOptions<Settings>().Bind(hostContext.Configuration.GetSection("Settings")));

using var host = builder.Build();
using var serviceScope = host.Services.CreateScope();

var provider = serviceScope.ServiceProvider;

var mainService = provider.GetRequiredService<MainService>();
await mainService.RunElectionsAsync();

//await host.RunAsync();