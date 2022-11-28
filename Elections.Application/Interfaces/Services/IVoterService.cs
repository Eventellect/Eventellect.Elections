using Elections.Domain.Interfaces;

namespace Elections.Application.Interfaces.Services
{
    public interface IVoterService
    {
        IReadOnlyList<IVoter> CreateVoters(int voterCount, IReadOnlyList<ICandidate> candidates);
    }
}
