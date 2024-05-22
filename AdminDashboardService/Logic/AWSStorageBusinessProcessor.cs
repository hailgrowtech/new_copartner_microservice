using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using AdminDashboardService.Dtos;
using MigrationDB.Model;
using AdminDashboardService.Queries;
using AdminDashboardService.Commands;
using Amazon.S3;
using Amazon.S3.Model;
using static MassTransit.ValidationResultExtensions;
using MassTransit.Caching.Internals;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdminDashboardService.Logic;
public class AWSStorageBusinessProcessor : IAWSStorageBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IAmazonS3 _s3Client;

    public AWSStorageBusinessProcessor(ISender sender, IMapper mapper, IAmazonS3 s3Client)
    {
        this._sender = sender;
        this._mapper = mapper;
        this._s3Client = s3Client;
    }

   

    public async Task<ResponseDto> Delete(string? fileName, string bucketName)
    {
        var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
        if (!bucketExists)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { $"Bucket {bucketName} does not exist." }
            };
        }

        if (string.IsNullOrEmpty(fileName))
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { "File name is required for deletion." }
            };
        }

        await _s3Client.DeleteObjectAsync(bucketName, fileName);
        var responseDto = new ResponseDto()
        {
            DisplayMessage = $"File {fileName} deleted from S3 successfully!"
        };
        return responseDto;
    }


    public async Task<ResponseDto> Post(IFormFile file, string? prefix, string bucketName)
    {
        var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
        if (!bucketExists)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ErrorMessages = new List<string>() { $"Bucket {bucketName} does not exist." }
            };
        }

        // Upload the file to Amazon S3
        var request = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
            InputStream = file.OpenReadStream()
        };
        request.Metadata.Add("Content-Type", file.ContentType);
        await _s3Client.PutObjectAsync(request);

        // Generate a presigned URL for the uploaded file
        var presignedUrlRequest = new GetPreSignedUrlRequest()
        {
            BucketName = bucketName,
            Key = request.Key,
            Expires = DateTime.UtcNow.AddMinutes(1) // Set the expiration time for the presigned URL
        };
        string presignedUrl = _s3Client.GetPreSignedURL(presignedUrlRequest);

        // Create an instance of AWSStorageReadDto and assign the presigned URL to it
        var resultDto = new AWSStorageReadDto
        {
            PresignedUrl = presignedUrl
        };

        // Prepare the response DTO
        var responseDto = new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = $"File {prefix}/{file.FileName} uploaded to S3 successfully!"
        };

        return responseDto;
    }


}

