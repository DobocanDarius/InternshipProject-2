namespace InternshipProject_2.Helpers;

public class TokenRevocation
{
    private readonly IDictionary<string, bool> _revokedTokens = new Dictionary<string, bool>();
    public void RevokeToken(string token)
    {
        _revokedTokens[token] = true;
    }
    public bool IsTokenRevoked(string token)
    {
        return _revokedTokens.ContainsKey(token);
    }
}
