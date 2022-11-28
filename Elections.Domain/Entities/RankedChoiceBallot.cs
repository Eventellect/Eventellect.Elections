using Elections.Domain.Interfaces;

namespace Elections.Domain.Entities;

public record RankedChoiceBallot(IVoter Voter, IReadOnlyList<IRankedVote> Votes) : IRankedBallot;

