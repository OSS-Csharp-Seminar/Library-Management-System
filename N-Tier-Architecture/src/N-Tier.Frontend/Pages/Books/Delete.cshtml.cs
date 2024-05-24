using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly IBookService _bookService; 

        public DeleteModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var book = await _bookService.GetById(id);
            if(book == null)
            {
                return base.BadRequest($"Unable to find book with id '{id}'");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var book = await _bookService.GetById(id);
            if (book == null)
            {
                return base.BadRequest($"Unable to find book with id '{id}'");
            }

            await _bookService.DeleteAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
