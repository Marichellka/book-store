using System.Security.Claims;

namespace BookStore.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        var identity = (ClaimsIdentity)principal.Identity!;

        string identifierName = identity.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        return int.Parse(identifierName);
    }
}