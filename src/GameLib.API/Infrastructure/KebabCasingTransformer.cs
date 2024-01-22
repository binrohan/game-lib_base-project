using System.Text.RegularExpressions;

namespace GameLib.API.Infrastructure;

public class KebabCasingTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value) => value != null 
        ? Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower()
        : null;
}
