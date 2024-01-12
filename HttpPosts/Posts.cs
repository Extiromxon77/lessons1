namespace HttpPost
{
    public record class Posts(
            int? UserId = null,
            int? Id = null,
            string? Title = null,
            string? Body = null
        );
}
