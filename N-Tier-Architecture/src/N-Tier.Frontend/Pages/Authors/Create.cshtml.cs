using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models.Author;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Authors
{
    [Authorize(Roles ="Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public CreateModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateAuthorModel Author { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _authorService.CreateAsync(Author);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
