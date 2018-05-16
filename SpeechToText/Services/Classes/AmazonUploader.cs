using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime.SharedInterfaces;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Hosting;

namespace Services.Classes
{
    public class AmazonUploader
    {
        private IHostingEnvironment _hostingEnvironment;
        private ICoreAmazonS3 _amazonS3Client; 
        private string _bucketName = "speechtotextcomparison2";
        private static readonly string BucketSubdirectory = String.Empty;

        public AmazonUploader(IHostingEnvironment environment, ICoreAmazonS3 AmazonS3Client)
        {
            _hostingEnvironment = environment;
            _amazonS3Client = AmazonS3Client;
        }

        public void Tester()
        {
            try
            {
                var x = _amazonS3Client.DoesS3BucketExistAsync(_bucketName);
            }
            catch (Exception ex)
            {
                var res = ex.Message;
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
