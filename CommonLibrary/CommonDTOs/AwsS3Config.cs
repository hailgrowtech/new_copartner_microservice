using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.CommonDTOs;

public  class AwsS3Config
{
    public string BucketName { get; set; }
    public string EncryptedAccessKey { get; set; }
    public string EncryptedSecretKey { get; set; }
    public string Region { get; set; }
}
