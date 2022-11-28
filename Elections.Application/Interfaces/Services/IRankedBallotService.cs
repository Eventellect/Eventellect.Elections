using Elections.Domain.Interfaces;

namespace Elections.Application.Interfaces.Services
{
    public interface IRankedBallotService
    {
        IReadOnlyList<IRankedBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates);
    }
}
