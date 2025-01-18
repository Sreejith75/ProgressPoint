// using Bytestrone.AppraisalSystem.Core.PermissionAggregate;
// using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;
// using System.Collections.Generic;
// using System.Linq;
// using Xunit;

// namespace Bytestrone.AppraisalSystem.Tests.SystemRoleAggregate
// {
//     public class SystemRoleTests
//     {
//         private readonly SystemRole _systemRole;

//         public SystemRoleTests()
//         {
//             _systemRole = new SystemRole("Admin", "Administrator role with full access");
//         }

//         [Fact]
//         public void Constructor_InitializesPropertiesCorrectly()
//         {
//             Assert.Equal("Admin", _systemRole.RoleName);
//             Assert.Equal("Administrator role with full access", _systemRole.Description);
//             Assert.Empty(_systemRole.EmployeeSystemRoles);
//             Assert.Empty(_systemRole.SystemRolePermissions);
//         }

//         [Fact]
//         public void AddPermission_ValidPermission_AddsPermission()
//         {
//             var permission = new Permission("PERM_VIEW", "View permissions");

//             _systemRole.AddPermission(permission);

//             Assert.Single(_systemRole.SystemRolePermissions);
//             Assert.Contains(_systemRole.SystemRolePermissions, sr => sr.PermissionId == permission.Id);
//         }

//         [Fact]
//         public void AddPermission_DuplicatePermission_DoesNotAddAgain()
//         {
//             var permission = new Permission("PERM_EDIT", "Edit permissions");

//             _systemRole.AddPermission(permission);
//             _systemRole.AddPermission(permission);

//             Assert.Single(_systemRole.SystemRolePermissions); // No duplicate
//         }

//         [Fact]
//         public void RemovePermission_ExistingPermission_RemovesPermission()
//         {
//             var permission = new Permission("PERM_DELETE", "Delete permissions");
//             _systemRole.AddPermission(permission);

//             _systemRole.RemovePermission(permission);

//             Assert.Empty(_systemRole.SystemRolePermissions);
//         }

//         [Fact]
//         public void RemovePermission_NonExistingPermission_DoesNothing()
//         {
//             var permission = new Permission("PERM_ADD", "Add permissions");

//             _systemRole.RemovePermission(permission);

//             Assert.Empty(_systemRole.SystemRolePermissions); // No removal since it doesn't exist
//         }

//         [Fact]
//         public void GetPermissions_ReturnsCorrectPermissions()
//         {
//             var permission1 = new Permission("PERM_1", "Permission 1");
//             var permission2 = new Permission("PERM_2", "Permission 2");

//             _systemRole.AddPermission(permission1);
//             _systemRole.AddPermission(permission2);

//             var permissions = _systemRole.GetPermissions();

//             Assert.Equal(2, permissions.Count);
//             Assert.Contains(permission1, permissions);
//             Assert.Contains(permission2, permissions);
//         }

//         [Fact]
//         public void UpdateDetails_ValidDetails_UpdatesRoleNameAndDescription()
//         {
//             var newRoleName = "Manager";
//             var newDescription = "Manager role with limited access";

//             _systemRole.UpdateDetails(newRoleName, newDescription);

//             Assert.Equal(newRoleName, _systemRole.RoleName);
//             Assert.Equal(newDescription, _systemRole.Description);
//         }

//         [Fact]
//         public void UpdateDetails_NullOrEmptyRoleName_ThrowsException()
//         {
//             Assert.Throws<Ardalis.GuardClauses.GuardClauseException>(() =>
//                 _systemRole.UpdateDetails(null, "Updated Description"));

//             Assert.Throws<Ardalis.GuardClauses.GuardClauseException>(() =>
//                 _systemRole.UpdateDetails("", "Updated Description"));
//         }

//         [Fact]
//         public void UpdateDetails_NullOrEmptyDescription_ThrowsException()
//         {
//             Assert.Throws<Ardalis.GuardClauses.GuardClauseException>(() =>
//                 _systemRole.UpdateDetails("Updated Role Name", null));

//             Assert.Throws<Ardalis.GuardClauses.GuardClauseException>(() =>
//                 _systemRole.UpdateDetails("Updated Role Name", ""));
//         }
//     }
// }
