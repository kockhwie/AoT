using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Xml.Linq;

namespace AOT.Services;

public class EnvironmentVariableXmlRepository : IXmlRepository
{
    private readonly string _envVarName;

    public EnvironmentVariableXmlRepository(string envVarName)
    {
        _envVarName = envVarName;
    }

    public IReadOnlyCollection<XElement> GetAllElements()
    {
        var xml = Environment.GetEnvironmentVariable(_envVarName);
        if (string.IsNullOrEmpty(xml))
        {
            return Array.Empty<XElement>();
        }

        return new[] { XElement.Parse(xml) };
    }

    public void StoreElement(XElement element, string friendlyName)
    {
        // On ephemeral environments like Render Free Tier, we cannot save back to an environment variable.
        // However, since we provide a pre-generated valid key that lasts 10 years, 
        // this method won't be called until the key actually expires.
    }
}
