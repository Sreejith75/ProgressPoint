// using System.Threading;
// using Ardalis.GuardClauses;
// using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
// using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
// using Bytestrone.AppraisalSystem.Core.Interfaces;
// using Bytestrone.AppraisalSystem.Core.PermissionAggregate;
// using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

// using Xunit;

// namespace Bytestrone.AppraisalSystem.Tests.Core.EmployeeAggregate;
// public class EmployeeTests
// {
//     private readonly Employee _employee;
//     private readonly Mock<IHashingService> _hashingServiceMock;

//     public EmployeeTests()
//     {
//         _hashingServiceMock = new Mock<IHashingService>();
//         _hashingServiceMock.Setup(h => h.HashPassword(It.IsAny<string>())).Returns((string password) => $"hashed_{password}");
//         _employee = new Employee("John", "Doe", "john.doe@example.com", "hashed_password", 1);
//     }

//     [Fact]
//     public void Constructor_InitializesPropertiesCorrectly()
//     {
//         Assert.Equal("John", _employee.FirstName);
//         Assert.Equal("Doe", _employee.LastName);
//         Assert.Equal("john.doe@example.com", _employee.Email);
//         Assert.True(_employee.IsActive);
//         Assert.Equal(1, _employee.EmployeeRoleId);
//     }

//     [Fact]
//     public void UpdateContactInfo_ValidPhoneNumber_UpdatesPhoneNumber()
//     {
//         var phoneNumber = "123-456-7890";
//         _employee.UpdateContactInfo(phoneNumber);

//         Assert.Equal(phoneNumber, _employee.PhoneNumber);
//     }

//     [Fact]
//     public void UpdatePassword_ValidPassword_UpdatesPasswordHash()
//     {
//         var newPassword = "newPassword123";

//         _employee.UpdatePassword(newPassword, _hashingServiceMock.Object);

//         _hashingServiceMock.Verify(h => h.HashPassword(newPassword), Timer.Equals);
//         Assert.Equal($"hashed_{newPassword}", _employee.PasswordHash);
//     }

//     [Fact]
//     public void AssignNewAppraiser_ValidAppraiser_AddsMapping()
//     {
//         var appraiser = new Employee("Jane", "Smith", "jane.smith@example.com", "hashed_password", 2);
//         var effectiveDate = DateTime.UtcNow;

//         _employee.AssignNewAppraiser(appraiser, effectiveDate);

//         Assert.Single(_employee.AppraiserMappings);
//         var mapping = _employee.AppraiserMappings.First();
//         Assert.Equal(_employee.Id, mapping.EmployeeId);
//         Assert.Equal(appraiser.Id, mapping.AppraiserId);
//         Assert.Equal(effectiveDate, mapping.EffectiveDate);
//     }

//     [Fact]
//     public void AddSystemRole_ValidSystemRoleId_AddsRole()
//     {
//         var roleId = 5;

//         _employee.AddSystemRole(roleId);

//         Assert.Single(_employee.SystemRoles);
//         Assert.Contains(_employee.SystemRoles, r => r.SystemRoleId == roleId);
//     }

//     [Fact]
//     public void AddSystemRole_DuplicateSystemRoleId_DoesNotAddRoleAgain()
//     {
//         var roleId = 5;
//         _employee.AddSystemRole(roleId);
//         _employee.AddSystemRole(roleId);

//         Assert.Single(_employee.SystemRoles); // Ensure no duplicate role added
//     }

//     [Fact]
//     public void RemoveSystemRole_ExistingSystemRoleId_RemovesRole()
//     {
//         var roleId = 5;
//         _employee.AddSystemRole(roleId);

//         _employee.RemoveSystemRole(roleId);

//         Assert.DoesNotContain(_employee.SystemRoles, r => r.SystemRoleId == roleId);
//     }

//     [Fact]
//     public void GetPermissions_ReturnsCorrectPermissions()
//     {
//         var roleId = 5;
//         var systemRole = new SystemRole("Admin");
//         systemRole.AddPermission(new Permission("PERM_1", "Permission 1"));
//         systemRole.AddPermission(new Permission("PERM_2", "Permission 2"));

//         var employeeSystemRole = new EmployeeSystemRole(_employee.Id, roleId) { SystemRole = systemRole };
//         _employee.AddSystemRole(roleId);

//         var permissions = _employee.GetPermissions();

//         Assert.Contains("PERM_1", permissions);
//         Assert.Contains("PERM_2", permissions);
//         Assert.Equal(2, permissions.Count);
//     }

//     [Fact]
//     public void GetFullName_ReturnsCorrectFullName()
//     {
//         var fullName = _employee.GetFullName();

//         Assert.Equal("John Doe", fullName);
//     }
// }
