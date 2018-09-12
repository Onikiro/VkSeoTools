using VkNet.Model;

namespace VkInstruments.Core
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long UserId { get; set; }
        public int ExpiresIn { get; set; }

        public static AccessToken FromString(string token, string expiresIn, string userId)
        {
            long.TryParse(userId, out long parseUserId);
            int.TryParse(expiresIn, out int parseExpiresIn);

            return new AccessToken
            {
                Token = string.IsNullOrEmpty(token) || token == "null" ? null : token,
                ExpiresIn = parseExpiresIn,
                UserId = parseUserId
            };
        }

        public ApiAuthParams ToApiAuthParams()
        {
            return new ApiAuthParams
            {
                AccessToken = Token,
                TokenExpireTime = ExpiresIn,
                UserId = UserId
            };
        }
    }
}
