using Azure.Core;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Identity;
using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HRLeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public string? UserId => _contextAccessor?.HttpContext?.User.FindFirstValue("uid");

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);

            if(employee == null || employee.Email == null)
            {
                throw new NotFoundException($"Employee with {userId} not found.", userId);
            }

            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var enployees = await _userManager.GetUsersInRoleAsync("Employee");
            return enployees.Select(employee => new Employee 
            {
                Email = employee.Email ?? throw new BadRequestException("Employee invalid"),
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            }).ToList();
        }
    }
}
