namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.List;
public interface IListSystemRoleQueryService
{
    Task<IEnumerable<SystemRoleDTO>> ListAsync();
}