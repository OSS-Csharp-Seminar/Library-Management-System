using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Works
{
    public class DeleteModel : PageModel
    {
        private readonly IWorkService _workService;

        public DeleteModel(IWorkService workService)
        {
            _workService = workService;
        }

        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var work = await _workService.GetById(id);
            if (work == null)
            {
                return base.BadRequest($"Unable to find work with id '{id}'");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var work = await _workService.GetById(id);
            if (work == null)
            {
                return base.BadRequest($"Unable to find work with id '{id}'");
            }

            await _workService.DeleteAsync(id);

            return Redirect("./Index");
        }
    }
}
