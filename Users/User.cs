using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Users;

namespace Users

{
    public record class User(
            int Id,
            string Name = "",
            string UserName = "",
            string Email = "",
            Address Address
    );

    public record class Address(
        string Street = "",
        string Suite = "",
        string City = "",
        string Zipcode = "",
        Geo Geo
    );

    public record Geo(
        double Lat,
        double Lng
    );
}
