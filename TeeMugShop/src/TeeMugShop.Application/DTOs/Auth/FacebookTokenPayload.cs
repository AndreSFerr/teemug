
namespace TeeMugShop.Application.DTOs.Auth
{
    public class FacebookTokenPayload
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public PictureData? Picture { get; set; }

        public class PictureData
        {
            public PictureInfo? Data { get; set; }
        }

        public class PictureInfo
        {
            public string? Url { get; set; }
        }
    }
}
