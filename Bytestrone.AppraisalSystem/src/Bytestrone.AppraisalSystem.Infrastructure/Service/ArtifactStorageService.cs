using Bytestrone.AppraisalSystem.Core.Interfaces;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Bytestrone.AppraisalSystem.Infrastructure.Service;
public class ArtifactStorageService : IArtifactStorageService
{
    private readonly string _storagePath;

    public ArtifactStorageService(IConfiguration configuration)
    {
        _storagePath = configuration.GetValue<string>("ArtifactStorage:StoragePath")
            ?? throw new InvalidOperationException("Storage path is not configured.");

        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<Artifact> SaveArtifactAsync(byte[] fileData, string contentType)
    {
        // Generate a unique file name
        var uniqueFileName = GenerateUniqueFileName(contentType);
        var filePath = Path.Combine(_storagePath, uniqueFileName);

        try
        {
            // Save the artifact to the storage path
            await File.WriteAllBytesAsync(filePath, fileData);

            // Return a new Artifact value object with the file path
            return new Artifact(filePath, contentType);
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to save artifact: {ex.Message}", ex);
        }
    }

    public async Task<(byte[] fileData, Artifact artifact)> GetArtifactAsync(string artifactPath)
    {
        if (!File.Exists(artifactPath))
            throw new FileNotFoundException("Artifact not found", artifactPath);

        try
        {
            var fileData = await File.ReadAllBytesAsync(artifactPath);
            var contentType = GetContentType(artifactPath);

            // Return the file data and the corresponding Artifact value object
            return (fileData, new Artifact(artifactPath, contentType));
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to retrieve artifact: {ex.Message}", ex);
        }
    }

    public Task DeleteArtifactAsync(string artifactPath)
    {
        if (!File.Exists(artifactPath))
            return Task.CompletedTask;

        try
        {
            File.Delete(artifactPath);
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to delete artifact: {ex.Message}", ex);
        }

        return Task.CompletedTask;
    }

    private string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        foreach (var c in invalidChars)
        {
            fileName = fileName.Replace(c, '_');
        }
        return fileName;
    }

    // Generate a unique file name, based on content type
    private string GenerateUniqueFileName(string contentType)
    {
        var uniqueId = Guid.NewGuid().ToString("N");

        // Get the file extension based on content type
        var extension = contentType switch
        {
            "application/pdf" => ".pdf",
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            _ => ".dat" // Default file extension
        };

        return $"{uniqueId}{extension}";
    }

    // Determine the content type based on the file extension
    private string GetContentType(string filePath)
    {
        return filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ? "application/pdf" :
               filePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || filePath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ? "image/jpeg" :
               filePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ? "image/png" :
               "application/octet-stream";  // Default content type
    }
}
