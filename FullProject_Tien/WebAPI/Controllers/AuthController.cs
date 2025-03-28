using Core.Application.DTOs;
using Core.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;

namespace APIController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Random _random;
        private readonly byte[] key = { 0xfa, 0xc8, 0x39, 0x91, 0x0d, 0x4b, 0x7f, 0xe5, 0x27, 0x9d, 0xa4, 0x75, 0xe9, 0x2f, 0xa2, 0x8c };
        private readonly byte[] iv = { 0x53, 0x29, 0x6e, 0xf7, 0x45, 0x81, 0x19, 0x7c, 0xd3, 0x8b, 0x9d, 0xe3, 0x2a, 0xe6, 0x8a, 0x09 };

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _random = new Random();
        }

        private string GenerateToken(string username, string role)
        {
            var userData = new
            {
                Username = username,
                Role = role,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), // Current timestamp in seconds
                Expiry = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() // Expiry in one hour
            };

            // Serialize data to JSON
            var jsonData = JsonSerializer.Serialize(userData);

            // Encrypt JSON data using AES
            var encryptedData = EncryptAES(jsonData, key, iv);

            // Encode the encrypted data as a base64 string
            var encodedToken = Convert.ToBase64String(encryptedData);

            return encodedToken;
        }

        private byte[] EncryptAES(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Write all data to the stream
                            swEncrypt.Write(plainText);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        private string DecryptAES(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /*private byte[] XOREncrypt(string data)
        {
            // Convert data and key to bytes
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var keyLength = key.Length;

            // Perform XOR encryption
            for (int i = 0; i < dataBytes.Length; i++)
            {
                dataBytes[i] ^= key[i % keyLength];
            }

            return dataBytes;
        }

        private string XORDecrypt(byte[] data)
        {
            var decryptedData = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                decryptedData[i] = (byte)(data[i] ^ key[i % key.Length]);
            }
            return Encoding.UTF8.GetString(decryptedData);
        }*/

        private bool IsValidToken(TokenData tokenData)
        {
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (tokenData.Timestamp > currentTime - (5 * 60)) // Allow a 5-minute tolerance
            {
                // Validate expiry: Check if the token has not expired
                if (tokenData.Expiry > currentTime)
                {
                    // Additional validation checks can be added here if needed
                    return true; // Token is valid
                }
            }
            return false;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult register(TaiKhoan taiKhoan)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
                SqlCommand cmd = new SqlCommand("INSERT INTO TaiKhoans(TenTaiKhoan, MatKhau, MaNV, Role) VALUES ('" + taiKhoan.TenTaiKhoan + "', '" + taiKhoan.MatKhau + "', '" + taiKhoan.MaNV + "', '" + taiKhoan.Role + "')", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    string token = GenerateToken(taiKhoan.TenTaiKhoan, taiKhoan.Role);
                    return Ok(new { Message = "Registration successful", Token = token });
                }
                else
                {
                    return Ok("Registration error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Register request failed");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(LoginModel loginModel)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
                SqlDataAdapter da = new SqlDataAdapter("SELECT * from TaiKhoans WHERE TenTaiKhoan = '" + loginModel.username + "' AND MatKhau = '" + loginModel.password + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string role = dt.Rows[0]["Role"].ToString();
                    string token = GenerateToken(loginModel.username, role);
                    return Ok(new { Message = "Login successful", Token = token });
                }
                else
                {
                    return Ok("Invalid login information");
                }
            }
            catch (Exception ex) 
            {
                return BadRequest("Login request failed");
            }
        }

        [HttpGet]
        [Route("ValidateToken")]
        public IActionResult ValidateToken(string token)
        {
            try
            {
                // Decode the token from base64
                var decodedToken = Convert.FromBase64String(token);

                // Decrypt the token using XOR cipher
                var decryptedData = DecryptAES(decodedToken, key, iv);

                // Deserialize JSON data
                var tokenData = JsonSerializer.Deserialize<TokenData>(decryptedData);

                // Validate the token data (e.g., check timestamp, expiry, etc.)
                if (IsValidToken(tokenData))
                {
                    return Ok(new { Message = "Token is valid", Username = tokenData.Username, Role = tokenData.Role, TimeStamp = tokenData.Timestamp.ToString(), Expiry = tokenData.Expiry.ToString() });
                }
                else
                {
                    return BadRequest("Invalid token");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid token format");
            }
        }
    }
}
