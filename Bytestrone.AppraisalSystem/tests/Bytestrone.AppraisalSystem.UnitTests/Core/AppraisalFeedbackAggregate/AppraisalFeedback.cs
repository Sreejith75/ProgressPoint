// using System;
// using System.Collections.Generic;
// using System.Text.Json;
// using Xunit;
// using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
// using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
// using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
// using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

// namespace Bytestrone.AppraisalSystem.Tests
// {
//     public class AppraisalFeedbackTests
//     {
//         [Fact]
//         public void CalculateAppraiseeScore_ShouldReturnExpectedScore()
//         {
//             // Arrange
//             var appraisalFeedback = CreateAppraisalFeedbackWithMockData();
            
//             // Act
//             var score = appraisalFeedback.CalculateAppraiseeScore();

//             // Assert
//             Assert.Equal(expected: 3.85m, actual: Math.Round(score, 2)); // Adjust expected score based on mock data
//         }

//         [Fact]
//         public void CalculateAppraiserScore_ShouldReturnExpectedScore()
//         {
//             // Arrange
//             var appraisalFeedback = CreateAppraisalFeedbackWithMockData();
            
//             // Act
//             var score = appraisalFeedback.CalculateAppraiserScore();

//             // Assert
//             Assert.Equal(expected: 3.95m, actual: Math.Round(score, 2)); // Adjust expected score based on mock data
//         }

//         private AppraisalFeedback CreateAppraisalFeedbackWithMockData()
//         {
//             // Sample mock JSON data for feedback details
//             var feedbackDetailsJson = @"
//             [
//                 {
//                     ""Question"": {
//                         ""PerformanceIndicatorId"": 1,
//                         ""Indicator"": {
//                             ""Id"": 1,
//                             ""Weightage"": 0.4,
//                             ""PerformanceFactor"": {
//                                 ""Id"": 1,
//                                 ""DepartmentPerformanceFactors"": [
//                                     { ""DepartmentId"": 1, ""Weightage"": 0.5 }
//                                 ]
//                             }
//                         }
//                     },
//                     ""AppraiseeRating"": 4.5,
//                     ""AppraiserRating"": 3.8
//                 },
//                 {
//                     ""Question"": {
//                         ""PerformanceIndicatorId"": 2,
//                         ""Indicator"": {
//                             ""Id"": 2,
//                             ""Weightage"": 0.6,
//                             ""PerformanceFactor"": {
//                                 ""Id"": 2,
//                                 ""DepartmentPerformanceFactors"": [
//                                     { ""DepartmentId"": 1, ""Weightage"": 0.7 }
//                                 ]
//                             }
//                         }
//                     },
//                     ""AppraiseeRating"": 3.5,
//                     ""AppraiserRating"": 4.2
//                 }
//             ]";

//             // Deserialize JSON into feedback details
//             var feedbackDetails = JsonSerializer.Deserialize<List<AppraisalFeedbackDetail>>(feedbackDetailsJson, new JsonSerializerOptions
//             {
//                 PropertyNameCaseInsensitive = true
//             });

//             if (feedbackDetails == null)
//                 throw new InvalidOperationException("Failed to parse mock feedback details.");

//             // Create an AppraisalFeedback instance
//             var appraisalFeedback = new AppraisalFeedback(1, 1);

//             // Mock Employee with a Role and Department
//             appraisalFeedback.Employee = new Employee
//             {
//                 Role = new EmployeeRole("Developer", "DEV001", 1, 1)
//             };

//             // Add feedback details to the AppraisalFeedback instance
//             foreach (var detail in feedbackDetails)
//             {
//                 appraisalFeedback.AddFeedbackDetail(detail);
//             }

//             return appraisalFeedback;
//         }
//     }
// }
