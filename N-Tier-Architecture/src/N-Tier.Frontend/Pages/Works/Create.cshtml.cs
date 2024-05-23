using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models.Author;
using N_Tier.Application.Models.Work;
using N_Tier.Application.Services;
using N_Tier.Core.Entities;

namespace N_Tier.Frontend.Pages.Works
{
    public class CreateModel : PageModel
    {
        private readonly IWorkService _workService;
        private readonly IAuthorService _authorService;

        public CreateModel(IWorkService workService, IAuthorService authorService)
        {
            _workService = workService;
            _authorService = authorService;
        }

        public IEnumerable<AuthorResponseModel> Authors { get; set; } = new List<AuthorResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Authors = await _authorService.GetAllAsync();

            return Page();
        }

        [BindProperty]
        public CreateWorkModel Work { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _workService.CreateAsync(Work);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
