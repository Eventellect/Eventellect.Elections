using Elections.Domain.Interfaces;

namespace Elections.Application.Interfaces.Services
{
    public interface ISingleBallotService
    {
        IReadOnlyList<ISingleVoteBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates);
    }
}
