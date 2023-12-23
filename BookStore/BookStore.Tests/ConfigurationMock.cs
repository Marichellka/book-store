using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace BookStore.Tests;

public class ConfigurationMock : IConfiguration
{
    private readonly Dictionary<string, string> _values;

    public ConfigurationMock(Dictionary<string, string> values)
    {
        _values = values;
    }

    public IEnumerable<IConfigurationSection> GetChildren() => throw new NotSupportedException();

    public IChangeToken GetReloadToken() => throw new NotSupportedException();

    public IConfigurationSection GetSection(string key) => throw new NotSupportedException();

    public string? this[string key]
    {
        get => _values[key];
        set => throw new NotImplementedException();
    }
}