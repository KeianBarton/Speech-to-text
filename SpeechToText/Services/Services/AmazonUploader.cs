using System;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Services.IServices;

namespace Services.Services
{
    public class AmazonUploader : IAmazonUploader
    {
        private IAmazonS3 _amazonS3Client; 
        private string _bucketName = "speechtotextcomparison2";
        private string _newBucketTest = "testbucketx746e73w12";
        private static readonly string BucketSubdirectory = String.Empty;

        public AmazonUploader(IAmazonS3 AmazonS3Client)
        {
            _amazonS3Client = AmazonS3Client;
        }

        public async Task<bool> Tester()
        {
            try
            {
                await CreateBucketAsync();
                var x = await _amazonS3Client.DoesS3BucketExistAsync(_bucketName);
                var z = 0;
                return x;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
            }

            return false;
        }

        public async Task CreateBucketAsync()
        {
            try
            {

                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = _newBucketTest,
                    UseClientRegion = true
                };

                PutBucketResponse putBucketResponse = await _amazonS3Client.PutBucketAsync(putBucketRequest);
                
                // Retrieve the bucket location.
                //string bucketLocation = await FindBucketLocationAsync(s3Client);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }


        //public void UploadToS3(string filePath)
        //{
        //    try
        //    {
        //        var fileTransferUtility = new TransferUtility(_amazonS3Client.);

        //        string bucketName;


        //        if (string.IsNullOrEmpty(BucketSubdirectory))
        //        {
        //            bucketName = _bucketName; //no subdirectory just bucket name  
        //        }
        //        else
        //        {   // subdirectory and bucket name  
        //            bucketName = _bucketName + @"/" + BucketSubdirectory;
        //        }


        //        // 1. Upload a file, file name is used as the object key name.
        //        fileTransferUtility.Upload(filePath, bucketName);
        //        Console.WriteLine("Upload 1 completed");


        //    }
        //    catch (AmazonS3Exception s3Exception)
        //    {
        //        Console.WriteLine(s3Exception.Message,
        //            s3Exception.InnerException);
        //    }
        //}
    }
}
