using Elections.Domain.Interfaces;

namespace Elections.Domain.Entities;

public record SimpleVote(ICandidate Candidate) : IVote;

