using Microsoft.VisualBasic.FileIO;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
public record ArtifactDto
(
    string FileName,
    byte[] FileData,
    string ContentType
);