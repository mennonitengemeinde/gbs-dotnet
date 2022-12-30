using Gbs.Application.Entities;

namespace Gbs.Tests.Infrastructure.Seeds;

public static class ChurchSeed
{
    public static List<Church> GetChurches()
    {
        return new List<Church>
        {
            new Church
            {
                Id = 1,
                Name = "Church 1",
                Address = "Address 1",
                City = "City 1",
                State = "State 1",
                PostalCode = "PostalCode 1",
                Country = "Country 1",
            },
            new Church
            {
                Id = 2,
                Name = "Church 2",
                Address = "Address 2",
                City = "City 2",
                State = "State 2",
                PostalCode = "PostalCode 2",
                Country = "Country 2",
            },
            new Church
            {
                Id = 3,
                Name = "Church 3",
                Address = "Address 3",
                City = "City 3",
                State = "State 3",
                PostalCode = "PostalCode 3",
                Country = "Country 3",
            },
        };
    }
}