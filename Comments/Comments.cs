namespace Comments
{
    public record class Comments(
            int? PostId = null,
            int? Id = null,
            string? Name = null,
            string? Email = null,
            string? Body = null
        );
}
