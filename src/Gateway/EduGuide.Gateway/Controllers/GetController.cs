using Base.Api.Base;
using Base.Application.Contracts;
using Base.Application.Contracts.DTOs.Common;
using EduGuide.Application.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace EduGuide.Gateway.Controllers
{
    public class GetController : BaseApiController
    {
        ////private readonly IValidator<GetQuery> _validator;
        //public GetController(IMediator mediator) : base(mediator)
        //{
        //    //_validator = validator;
        //}

        //[HttpGet]
        ////[Authorize(Roles = "Student")]
        //public async Task<Result<bool>> GetTestData([FromQuery] GetQuery query)
        //{
        //    return await Mediator.Send(query);
        //}
        private readonly IMinioClient _minio;
        private const string BucketName = "eduguide";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMinIoService _minIoService;
        public GetController(IMinioClient minio, IMediator mediator, IHttpClientFactory httpClientFactory, IMinIoService minIoService) : base(mediator)
        {
            _minio = minio;
            _httpClientFactory = httpClientFactory;
            _minIoService = minIoService;
        }

        [HttpGet("stream")]
        public async Task<IActionResult> GetFileStream([FromQuery] string filePath, [FromQuery] string fileName)
        {
            try
            {
                // 1. Get presigned download URL from MinIO
                var downloadUrl = await _minIoService.GetDownloadUrl(fileName, filePath);

                // 2. Create HttpClient with timeout
                using var httpClient = _httpClientFactory.CreateClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                // 3. Get the file stream (without buffering)
                var response = await httpClient.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                // 4. Return as octet-stream (will download in browser)
                return File(await response.Content.ReadAsStreamAsync(), "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving file");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> StreamImage([FromQuery] string fileUrl)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                Stream file = await response.Content.ReadAsStreamAsync(); ;

                var contentType = response.Content.Headers.ContentType?.MediaType;

                return File(
                    file,
                    contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error streaming image: {ex.Message}");
            }
        }

        private string GetContentTypeFromFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }


        [HttpPost("upload")]
        public async Task<Result<string>> Upload([FromForm] GetQuery command)
        {
            return await Mediator.Send(command);
        }


        //[HttpGet("download/{filename}")]
        //public async Task<IActionResult> Download(string filename)
        //{
        //    try
        //    {
        //        // First check if the bucket exists
        //        var bucketExists = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(BucketName));
        //        if (!bucketExists)
        //            return NotFound($"Bucket '{BucketName}' does not exist");

        //        // Check if the object exists
        //        try
        //        {
        //            await _minio.StatObjectAsync(new StatObjectArgs()
        //                .WithBucket(BucketName)
        //                .WithObject(filename));
        //        }
        //        catch (Exception)
        //        {
        //            return NotFound($"File '{filename}' not found in bucket '{BucketName}'");
        //        }

        //        var ms = new MemoryStream();
        //        await _minio.GetObjectAsync(new GetObjectArgs()
        //            .WithBucket(BucketName)
        //            .WithObject($"CounselorRecruitment/{filename}")
        //            .WithCallbackStream(stream => stream.CopyTo(ms)));

        //        ms.Position = 0;
        //        return File(ms, "application/octet-stream", filename);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error downloading file: {ex.Message}");
        //    }
        //}
        //[HttpGet("download-url")]
        //public async Task<Result<bool>> GetDownloadUrl([FromQuery] GetQuery query)
        //{
        //    return await Mediator.Send(query);  
        //}
    }
}