using VkNet.Model;

namespace VkInstruments.Core
{
    public class AccessToken
    {
        public string Token { get; private set; }
        public long UserId { get; private set; }
        public int ExpiresIn { get; private set; }

        public static AccessToken FromString(string token, string expiresIn, string userId)
        {
            long.TryParse(userId, out var parseUserId);
            int.TryParse(expiresIn, out var parseExpiresIn);

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
