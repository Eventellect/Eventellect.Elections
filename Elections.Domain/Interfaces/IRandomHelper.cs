namespace Elections.Domain.Interfaces;

    public interface IRandomHelper
    {
        ICandidate SelectRandom(IReadOnlyList<ICandidate> candidates);
    }
