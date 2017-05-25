using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;

namespace Codetecuico.Byns.Common.AzureStorage
{
    public class AzureStorage
    {
        private readonly string _container;
        private readonly string _connectionString;

        public AzureStorage()
        {
            _connectionString = "StorageConnectionString";
            _container = "byns";
        }

        public bool Upload(string path = "")
        {
            // Retrieve storage account from connection string.
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(_connectionString));

            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            var container = blobClient.GetContainerReference(_container);

            // Retrieve reference to a blob named "myblob".
            var blockBlob = container.GetBlockBlobReference("ct.png");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(path))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            return true;
        }

        public bool Download()
        {
            // Retrieve storage account from connection string.
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(_connectionString));

            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            var container = blobClient.GetContainerReference(_container);

            // Retrieve reference to a blob named "photo1.jpg".
            var blockBlob = container.GetBlockBlobReference("xamarin.png");

            // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\ian.peter.s.tecuico\Downloads\xamarin.png"))
            {
                blockBlob.DownloadToStream(fileStream);
            }

            return true;
        }
    }
}
