using Elections.Application.Interfaces.Services;
using Elections.Application.Services;
using Elections.Domain.Interfaces;
using Elections.Domain.Interfaces.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Elections.Infrastructure.IoC
{
    public class DependencyContainer
    {    
        public ServiceProvider RegisterServices(IServiceCollection services)
        {
            //Domain.Interfaces.Repo | Infrastructure.Repo
            services.AddScoped<IRandomHelper, RandomHelper>();
            services.AddScoped<ICandidateRepo, CandidateRepo>();
            services.AddScoped<IVoterRepo, VoterRepo>();
            services.AddScoped<IRankedBallotRepo, RankedBallotRepo>();
            services.AddScoped<ISingleVoteBallotRepo, SingleVoteBallotRepo>();
            
            //Application
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IVoterService, VoterService>();            
            services.AddScoped<IRankedBallotService, RankedBallotService>();
            services.AddScoped<ISingleBallotService, SingleBallotService>();

            return services.BuildServiceProvider();
        }
        
    }
}
