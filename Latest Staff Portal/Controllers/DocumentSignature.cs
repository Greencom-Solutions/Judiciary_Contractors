using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Latest_Staff_Portal.Models;
using ZXing;
using ZXing.QrCode;

namespace Latest_Staff_Portal.Controllers
{
    public class DocumentSignatureController : Controller
    {
        [HttpPost]
        public JsonResult GenerateDigitalSignature(SignatureRequest request)
        {
            if (request == null) return Json(new { Success = false, Message = "Request cannot be null" });

            if (string.IsNullOrEmpty(request.UserID))
                return Json(new { Success = false, Message = "UserID cannot be null or empty" });

            if (string.IsNullOrEmpty(request.Name))
                return Json(new { Success = false, Message = "Name cannot be null or empty" });

            if (string.IsNullOrEmpty(request.Otp))
                return Json(new { Success = false, Message = "OTP cannot be null or empty" });

            if (string.IsNullOrEmpty(request.DocumentNo))
                return Json(new { Success = false, Message = "DocumentNo cannot be null or empty" });

            if (string.IsNullOrEmpty(request.FileName))
                return Json(new { Success = false, Message = "FileName cannot be null or empty" });

            if (request.TableId <= 0)
                return Json(new { Success = false, Message = "TableId must be greater than zero" });

            try
            {
                // Retrieve the user's signature from the credentials system
                var userSign = Credentials.ObjNav.GetUserSignature(request.UserID);
                if (string.IsNullOrEmpty(userSign))
                    return Json(new { Success = false, Message = "User signature not found" });

                // Generate the digital signature data string
                var currentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                var dataToSign = $"{request.UserID}|{request.Name}|{request.Otp}|{currentTime}|{userSign}";

                // Compute the SHA-256 hash of the data to be signed
                using var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
                var base64Signature = Convert.ToBase64String(hashBytes);

                // Generate a QR code from the base64-encoded digital signature
                var qrWriter = new BarcodeWriter<Bitmap>
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Height = 300,
                        Width = 300
                    }
                };

                var qrCodeImage = qrWriter.Write(base64Signature);
                using var ms = new MemoryStream();
                qrCodeImage.Save(ms, ImageFormat.Png);
                var base64QRCode = Convert.ToBase64String(ms.ToArray());

                if (string.IsNullOrEmpty(base64QRCode))
                    return Json(new
                        { Success = false, Message = "Failed to generate QR code. Please try again." });

                // Upload the QR code to the approval document
                Credentials.ObjNav.UploadApprovalQRDocument(request.UserID, request.DocumentNo,
                    request.FileName, base64QRCode, request.TableId);

                return Json(new
                    { QRCode = base64QRCode, Success = true, Message = "Document signed successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}