using Elections.Voters.Interfaces;

namespace Elections.Voters.Records;

public record Candidate(int Id, string Name) : ICandidate, IVoter;