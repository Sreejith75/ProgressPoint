using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Bytestrone.AppraisalSystem.Web.EmployeeRoles;
public class CreateEmployeeRoleRequest
{
    public const string Route="/EmployeeRoles";

    [Required]
    public string? RoleName { get; set; }
    public string? EmployeeRoleCode { get; set; }
    public int HierarchyLevel { get; set; }
    public int  DepartmentId { get; set; }
}