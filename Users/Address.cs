namespace Users

{
    public record Address(
        string Street = "",
        string Suite = "",
        string City = "",
        string Zipcode = "",
        Geo Geo
    );
}
