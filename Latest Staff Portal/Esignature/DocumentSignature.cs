using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Latest_Staff_Portal.Controllers
{
    public static class InternalDocumentSignature
    {
        [AcceptVerbs(HttpVerbs.Post)]
        public static string GenerateDigitalSignature(string name, string otp, string imgsign)
        {
            var currentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var dataToSign = $"{name}|{otp}|{currentTime}|{imgsign}";


            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
            var base64Signature = Convert.ToBase64String(hashBytes);

            return base64Signature;
        }
    }
}