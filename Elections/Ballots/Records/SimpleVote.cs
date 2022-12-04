using Elections.Ballots.Interfaces;
using Elections.Voters.Interfaces;

namespace Elections.Ballots.Records;

public record SimpleVote(ICandidate Candidate) : IVote;