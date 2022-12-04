using Elections.Voters.Interfaces;

namespace Elections.Voters.Records;

public record Voter(int Id, string Name) : IVoter;