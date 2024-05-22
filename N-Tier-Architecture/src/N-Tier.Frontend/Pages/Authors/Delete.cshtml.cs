using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public DeleteModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
            {
                return base.BadRequest($"Unable to find author with id '{id}'");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var author = await _authorService.GetById(id);
            if(author == null)
            {
                return base.BadRequest($"Unable to find author with id '{id}'");
            }

            await _authorService.DeleteAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
