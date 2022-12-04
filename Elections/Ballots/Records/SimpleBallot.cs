using Elections.Ballots.Interfaces;
using Elections.Voters.Interfaces;

namespace Elections.Ballots.Records;

public record SimpleBallot(IVoter Voter, IVote Vote) : ISingleVoteBallot;