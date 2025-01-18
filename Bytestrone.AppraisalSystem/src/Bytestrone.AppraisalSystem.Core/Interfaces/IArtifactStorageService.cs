using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;

namespace Bytestrone.AppraisalSystem.Core.Interfaces;

public interface IArtifactStorageService
{
    Task<Artifact> SaveArtifactAsync( byte[] fileData, string contentType);
    Task<(byte[] fileData, Artifact artifact)> GetArtifactAsync(string artifactPath);
    Task DeleteArtifactAsync(string artifactPath);
}
