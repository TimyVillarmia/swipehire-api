using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

public class AzureBlobService
{
    private readonly string? _connectionString;
    private readonly string? _containerName;

    public AzureBlobService(IConfiguration configuration)
    {
        _connectionString = configuration["AzureBlobStorage:ConnectionString"];
        _containerName = configuration["AzureBlobStorage:ContainerName"];

        if (string.IsNullOrWhiteSpace(_connectionString))
            throw new ArgumentNullException(nameof(_connectionString), "Azure Blob Storage connection string is required.");

        if (string.IsNullOrWhiteSpace(_containerName))
            throw new ArgumentNullException(nameof(_containerName), "Azure Blob Storage container name is required.");
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        BlobContainerClient container = new BlobContainerClient(_connectionString, _containerName);
        await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

        string fileName = $"{Guid.NewGuid()}_{file.FileName}";
        BlobClient blobClient = container.GetBlobClient(fileName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        }

        return fileName;
    }

    public string GetFileUrl(string fileName)
    {
        return $"https://swipehire.blob.core.windows.net/{_containerName}/{fileName}";
    }

    // Add DeleteFileAsync method
    public async Task<bool> DeleteFileAsync(string fileName)
    {
        BlobContainerClient container = new BlobContainerClient(_connectionString, _containerName);
        BlobClient blobClient = container.GetBlobClient(fileName);

        return await blobClient.DeleteIfExistsAsync();
    }
}
