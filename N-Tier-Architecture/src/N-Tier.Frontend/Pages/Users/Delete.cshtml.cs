using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Core.Entities.Identity;

namespace N_Tier.Frontend.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userId = new Guid(id);

            if (user == null)
            {
                return base.BadRequest($"Unable to find user with ID '{id}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return base.BadRequest($"Unable to find user with ID '{id}'.");
            }

            if(!ModelState.IsValid)
            {
                return Page();
            }

            await userManager.DeleteAsync(user);

            return Redirect("~/Users/Index");
        }
    }
}
