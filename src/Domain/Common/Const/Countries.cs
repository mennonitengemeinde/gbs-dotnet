namespace Gbs.Domain.Common.Const;

public static class Countries
{
    public const Country Canada = new Country("CA", "Canada", "https://en.wikipedia.org/wiki/Canada#/media/File:Flag_of_Canada_(Pantone).svg");
}

public record Country(string IsoCode, string Name, string Flag, string PhoneCode, List<State> States);

public record State(string Code, string Name);