using Elections.Domain.Interfaces;

namespace Elections.Application.Interfaces.Services
{
    public interface ICandidateService
    {
        IReadOnlyList<ICandidate> GetCandidates();
    }
}
