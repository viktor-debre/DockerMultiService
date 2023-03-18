using DockerDotNetService.Models.Hashing;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Digests;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace DockerDotNetService.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class HashingController : ControllerBase
    {
        private readonly ILogger<HashingController> _logger;

        public HashingController(
            ILogger<HashingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ActionName("sha-256")]
        public ActionResult<HashingResponce> HashBySHA256([FromBody] HashingData request)
        {
            var array = request.array;
            var timer = new Stopwatch();
            List<string> result = new List<string>(array.Length);
            timer.Start();
            foreach(var str in request.array)
            {
                result.Add(ComputeSha256Hash(str));
            }
            timer.Stop();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            var responce = new HashingResponce()
            {
                array = result,
                timeElapsed = timer.ElapsedTicks * nanosecPerTick,
            };

            return Ok(responce);
        }

        [HttpPost]
        [ActionName("sha3-256")]
        public ActionResult<HashingResponce> HashBySHA3_256([FromBody] HashingData request)
        {
            var array = request.array;
            var timer = new Stopwatch();
            List<string> result = new List<string>(array.Length);
            timer.Start();
            foreach (var str in request.array)
            {
                result.Add(ComputeSHA3Hash(str));
            }
            timer.Stop();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            var responce = new HashingResponce()
            {
                array = result,
                timeElapsed = timer.ElapsedTicks * nanosecPerTick,
            };

            return Ok(responce);
        }

        [HttpPost]
        [ActionName("sha-1")]
        public ActionResult<HashingResponce> HashBySHA1([FromBody] HashingData request)
        {
            var array = request.array;
            var timer = new Stopwatch();
            List<string> result = new List<string>(array.Length);
            timer.Start();
            foreach (var str in request.array)
            {
                result.Add(ComputeSHA3Hash(str));
            }
            timer.Stop();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            var responce = new HashingResponce()
            {
                array = result,
                timeElapsed = timer.ElapsedTicks * nanosecPerTick,
            };

            return Ok(responce);
        }

        public string ComputeSha256Hash(string input)
        {
            // Create a new instance of the SHA256Managed class
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    result.Append(hashBytes[i].ToString("x2"));
                }

                return result.ToString();
            }
        }

        public string ComputeSHA3Hash(string input)
        {
            // Hash the input using SHA3-256
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Sha3Digest sha3 = new Sha3Digest(256);
            byte[] hashBytes = new byte[sha3.GetDigestSize()];
            sha3.BlockUpdate(inputBytes, 0, inputBytes.Length);
            sha3.DoFinal(hashBytes, 0);

            // Convert the hash to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            string hashString = sb.ToString();

            // Return the hash as a string
            return hashString;
        }

        public string ComputeSha1Hash(string input)
        {
            // Create a new instance of the SHA1Managed class
            using (SHA1 sha1 = SHA1.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    result.Append(hashBytes[i].ToString("x2"));
                }

                return result.ToString();
            }
        }
    }
}
