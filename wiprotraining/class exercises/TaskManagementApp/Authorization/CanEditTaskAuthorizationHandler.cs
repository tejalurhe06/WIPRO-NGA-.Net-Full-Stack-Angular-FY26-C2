using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TaskManagementApp.Models;
using System.Threading.Tasks;

namespace TaskManagementApp.Authorization
{
    public class CanEditTaskAuthorizationHandler : AuthorizationHandler<EditTaskRequirement, TaskItem>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CanEditTaskAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EditTaskRequirement requirement, TaskItem resource)
        {
            var currentUser = await _userManager.GetUserAsync(context.User);

            // Admins can do anything
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return;
            }

            // Users can only edit their own tasks
            if (currentUser != null && resource.UserId == currentUser.Id)
            {
                context.Succeed(requirement);
            }
        }
    }
}