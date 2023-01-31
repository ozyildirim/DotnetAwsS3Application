using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAwsS3Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BucketController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;

        public BucketController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBucketAsync(string bucketName)
        {
            bool bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (bucketExists)
            {
                return BadRequest($"Bucket {bucketName} already exists.");
            }
            await _amazonS3.PutBucketAsync(bucketName);
            return Ok($"Bucket {bucketName} created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBucketsAsync()
        {
            ListBucketsResponse bucketDatas = await _amazonS3.ListBucketsAsync();
            return Ok(bucketDatas);
        }

        [HttpDelete("{bucketName}")]
        public async Task<IActionResult> DeleteBucketAsync(string bucketName)
        {
            await _amazonS3.DeleteBucketAsync(bucketName);
            return NoContent();
        }
    }
}
