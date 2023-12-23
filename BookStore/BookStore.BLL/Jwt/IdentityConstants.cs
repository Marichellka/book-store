namespace BookStore.BLL.Jwt;

public static class IdentityConstants
{
    public const string CONFIG_SECTION = "JwtSettings";

    public const string ISSUER = "Issuer";

    public const string CONFIG_SECTION_ISSUER = $"{CONFIG_SECTION}:{ISSUER}";

    public const string AUDIENCE = "Audience";

    public const string CONFIG_SECTION_AUDIENCE = $"{CONFIG_SECTION}:{AUDIENCE}";

    public const string KEY = "Key";
 
    public const string CONFIG_SECTION_KEY = $"{CONFIG_SECTION}:{KEY}";
}