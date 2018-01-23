using Microsoft.AspNet.Identity;
using System.Web.Configuration;
using System.Web.Security;

namespace CapstoneData.Utility
{
    public class MD5Hasher : PasswordHasher
    {
        public MD5Hasher(FormsAuthPasswordFormat format)
        {
            Format = format;
        }
        public FormsAuthPasswordFormat Format { get; set; }
        public override string HashPassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, Format.ToString());
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var testHash = FormsAuthentication.HashPasswordForStoringInConfigFile(providedPassword, Format.ToString());
            return testHash.Equals(hashedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
