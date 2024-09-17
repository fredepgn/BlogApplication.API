using Microsoft.AspNetCore.Identity;

namespace BlogApplication.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);

    }
}
