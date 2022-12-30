namespace Gbs.Shared.Common.Const;

public static class Countries
{
    public static Country Canada = new Country(
        "CA",
        "Canada",
        "+1",
        new List<Province>
        {
            new Province("AB", "Alberta"),
            new Province("BC", "British Columbia"),
            new Province("MB", "Manitoba"),
            new Province("NB", "New Brunswick"),
            new Province("NL", "Newfoundland and Labrador"),
            new Province("NS", "Nova Scotia"),
            new Province("NT", "Northwest Territories"),
            new Province("NU", "Nunavut"),
            new Province("ON", "Ontario"),
            new Province("PE", "Prince Edward Island"),
            new Province("QC", "Quebec"),
            new Province("SK", "Saskatchewan"),
        });

    public static Country Mexico = new Country(
        "MX",
        "Mexico",
        "+52",
        new List<Province>
        {
            new Province("AGU", "Aguascalientes"),
            new Province("BCN", "Baja California"),
            new Province("BCS", "Baja California Sur"),
            new Province("CAM", "Campeche"),
            new Province("CHP", "Chiapas"),
            new Province("CHH", "Chihuahua"),
            new Province("COA", "Coahuila"),
            new Province("COL", "Colima"),
            new Province("DIF", "Distrito Federal"),
            new Province("DUR", "Durango"),
            new Province("GUA", "Guanajuato"),
            new Province("GRO", "Guerrero"),
            new Province("HID", "Hidalgo"),
            new Province("JAL", "Jalisco"),
            new Province("MEX", "Mexico"),
            new Province("MIC", "Michoacan"),
            new Province("MOR", "Morelos"),
            new Province("NAY", "Nayarit"),
            new Province("NLE", "Nuevo Leon"),
            new Province("OAX", "Oaxaca"),
            new Province("PUE", "Puebla"),
            new Province("QUE", "Queretaro"),
            new Province("ROO", "Quintana Roo"),
            new Province("SLP", "San Luis Potosi"),
            new Province("SIN", "Sinaloa"),
            new Province("SON", "Sonora"),
            new Province("TAB", "Tabasco"),
            new Province("TAM", "Tamaulipas"),
            new Province("TLA", "Tlaxcala"),
            new Province("VER", "Veracruz"),
            new Province("YUC", "Yucatan"),
            new Province("ZAC", "Zacatecas"),
        });

    public static Country UnitedStates = new Country(
        "US",
        "United States",
        "+1",
        new List<Province>
        {
            new Province("AL", "Alabama"),
            new Province("AK", "Alaska"),
            new Province("AZ", "Arizona"),
            new Province("AR", "Arkansas"),
            new Province("CA", "California"),
            new Province("CO", "Colorado"),
            new Province("CT", "Connecticut"),
            new Province("DE", "Delaware"),
            new Province("DC", "District of Columbia"),
            new Province("FL", "Florida"),
            new Province("GA", "Georgia"),
            new Province("HI", "Hawaii"),
            new Province("ID", "Idaho"),
            new Province("IL", "Illinois"),
            new Province("IN", "Indiana"),
            new Province("IA", "Iowa"),
            new Province("KS", "Kansas"),
            new Province("KY", "Kentucky"),
            new Province("LA", "Louisiana"),
            new Province("ME", "Maine"),
            new Province("MD", "Maryland"),
            new Province("MA", "Massachusetts"),
            new Province("MI", "Michigan"),
            new Province("MN", "Minnesota"),
            new Province("MS", "Mississippi"),
            new Province("MO", "Missouri"),
            new Province("MT", "Montana"),
            new Province("NE", "Nebraska"),
            new Province("NV", "Nevada"),
            new Province("NH", "New Hampshire"),
            new Province("NJ", "New Jersey"),
            new Province("NM", "New Mexico"),
            new Province("NY", "New York"),
            new Province("NC", "North Carolina"),
            new Province("ND", "North Dakota"),
            new Province("OH", "Ohio"),
            new Province("OK", "Oklahoma"),
            new Province("OR", "Oregon"),
            new Province("PA", "Pennsylvania"),
            new Province("RI", "Rhode Island"),
            new Province("SC", "South Carolina"),
            new Province("SD", "South Dakota"),
            new Province("TN", "Tennessee"),
            new Province("TX", "Texas"),
            new Province("UT", "Utah"),
            new Province("VT", "Vermont"),
            new Province("VA", "Virginia"),
            new Province("WA", "Washington"),
            new Province("WV", "West Virginia"),
            new Province("WI", "Wisconsin"),
            new Province("WY", "Wyoming"),
        });
}

public readonly record struct Country(string Code, string Name, string PhoneCode, List<Province> Provinces);

public readonly record struct Province(string Code, string Name);