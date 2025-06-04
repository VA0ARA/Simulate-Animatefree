using System.Runtime.InteropServices;
using System.Security.Claims;

namespace AFAPIUnity.TokenService
{
    public interface IGenerateToken
    {
        string CtrateToken(IEnumerable<Claim> claims, DateTime expireAt);
        string CreateGusetToken( DateTime expireAt);
       // List<Claim> CreateClaims();
    }
}
