using Elections.Ballots.Interfaces;
using Elections.Voters.Interfaces;

namespace Elections.Ballots.Records;

public record RankedChoiceBallot(IVoter Voter, IReadOnlyList<IRankedVote> Votes) : IRankedBallot;