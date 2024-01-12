namespace Photos

{
    public record class Photos(
            int? AlbumId = null,
            int? Id = null,
            string? Title = null,
            string? Url = null,
            string? ThumbnailUrl = null
        );
}
