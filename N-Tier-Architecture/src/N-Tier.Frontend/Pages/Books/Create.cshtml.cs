using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models.Book;
using N_Tier.Application.Models.Work;
using N_Tier.Application.Services;
using N_Tier.Core.Entities;

namespace N_Tier.Frontend.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IWorkService _workService;

        public CreateModel(IBookService bookService, IWorkService workService)
        {
            _bookService = bookService;
            _workService = workService;
        }

        public IEnumerable<WorkResponseModel> Works { get; set; } = new List<WorkResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Works = await _workService.GetAllAsync();

            return Page();
        }

        [BindProperty]
        public CreateBookModel Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _bookService.CreateAsync(Book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
