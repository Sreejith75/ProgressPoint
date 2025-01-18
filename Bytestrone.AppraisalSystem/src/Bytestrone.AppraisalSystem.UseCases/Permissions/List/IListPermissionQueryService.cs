namespace Bytestrone.AppraisalSystem.UseCases.Permissions.List;
public interface IListPermissionQueryService
{
      Task<IEnumerable<PermissionDTO>> ListAsync();

}