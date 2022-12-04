using Elections.Ballots.Interfaces;
using Elections.Voters.Interfaces;

namespace Elections.Ballots.Records;

public record RankedChoiceVote(ICandidate Candidate, int Rank) : IRankedVote;