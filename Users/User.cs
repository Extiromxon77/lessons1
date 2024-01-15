using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Users;

namespace Users

{
    public record class User(
            int Id,
            Address Address,
            string Name = "",
            string UserName = "",
            string Email = ""
    );

    public record Adress(
        Geo Geo,
        string Street = "",
        string Suite = "",
        string City = "",
        string Zipcode = ""
     );

    public record Geo(
        double Lat,
        double Lng
    );
}
