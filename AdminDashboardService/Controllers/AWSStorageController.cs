using AdminDashboardService.Logic;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using CommonLibrary.CommonDTOs;
using CommonLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminDashboardService.Controllers;

[Route("api/[controller]")]
[ApiController]
[RequestSizeLimit(100_000_000)]
public class AWSStorageController : ControllerBase
{
    private readonly IAWSStorageBusinessProcessor _logic;
    private readonly ILogger<AWSStorageController> _logger;
    private readonly IConfiguration _configuration;

    public AWSStorageController(IAWSStorageBusinessProcessor logic, ILogger<AWSStorageController> logger,IConfiguration configuration)
    {
        _logic = logic;
        _logger = logger;
        _configuration = configuration;
    }
    /// <summary>
    /// Upload Files in Amazon S3 Staorage 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="prefix">Name of Folder For Images user Images for video use Videos</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file, string? prefix)
    {
        _logger.LogInformation("Uploading Data in AWS S3 Bucket..");
        var AwsS3Config = _configuration.GetSection("AwsS3Config").Get<AwsS3Config>(); //_configuration["AWSS3Credentials:BucketName"];
        string bucketName = EncryptionHelper.DecryptString(AwsS3Config.BucketName);
        var response = await _logic.Post(file, prefix, bucketName);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
    /// <summary>
    /// Delete File from AWS S3 Storage
    /// </summary>
    /// <param name="filePath">Images/abc.jpg</param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> Delete(string filePath)
    {
        var AwsS3Config = _configuration.GetSection("AwsS3Config").Get<AwsS3Config>();
        string bucketName = EncryptionHelper.DecryptString(AwsS3Config.BucketName);
        var response = await _logic.Delete(filePath, bucketName);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
}
