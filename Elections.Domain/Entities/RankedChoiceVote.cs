using Elections.Domain.Interfaces;

namespace Elections.Domain.Entities;

public record RankedChoiceVote(ICandidate Candidate, int Rank) : IRankedVote;

