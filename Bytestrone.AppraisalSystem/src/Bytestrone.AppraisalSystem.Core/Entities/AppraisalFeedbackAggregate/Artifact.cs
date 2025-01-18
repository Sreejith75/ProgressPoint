using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;

public class Artifact : ValueObject
{
    public string FilePath { get; private set; }
    public string FileType { get; private set; }

    public Artifact(string filePath, string fileType)
    {
        Guard.Against.NullOrEmpty(filePath, nameof(filePath));
        Guard.Against.NullOrEmpty(fileType, nameof(fileType));

        FilePath = filePath;
        FileType = fileType;
    }

    public Artifact Update(string newFilePath, string newFileType)
    {
        Guard.Against.NullOrEmpty(newFilePath, nameof(newFilePath));
        Guard.Against.NullOrEmpty(newFileType, nameof(newFileType));

        return new Artifact(newFilePath, newFileType);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FilePath;
        yield return FileType;
    }
}
