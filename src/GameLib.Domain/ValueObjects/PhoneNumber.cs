namespace GameLib.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; private set; }
    public string Number { get; private set; }
    public string Extension { get; private set; }

    public PhoneNumber(string number, string? countryCode = "", string? extension = "")
    {
        Number = number.Trim();
        CountryCode = string.IsNullOrWhiteSpace(countryCode) ? "" : countryCode.Trim();
        Extension = string.IsNullOrWhiteSpace(extension) ? "" : extension.Trim();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
        yield return Extension;
    }
}
