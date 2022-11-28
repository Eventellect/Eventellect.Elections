using Elections.Domain.Interfaces;

namespace Elections.Domain.Entities;

public record SimpleBallot(IVoter Voter, IVote Vote) : ISingleVoteBallot;

