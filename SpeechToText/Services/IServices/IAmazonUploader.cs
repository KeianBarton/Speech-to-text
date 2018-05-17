using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Models;
using Services.Services;

namespace Services.IServices
{
    public interface IAmazonUploader
    {
       Task<AmazonUploader.S3UploadResponse> UploadBase64Wav(string base64);
    }
}
