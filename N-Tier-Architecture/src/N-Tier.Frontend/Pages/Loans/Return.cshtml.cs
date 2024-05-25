using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Loans
{
    [Authorize(Roles =("Customer"))]
    public class ReturnModel : PageModel
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;

        public ReturnModel(ILoanService loanService, IBookService bookService)
        {
            _loanService = loanService;
            _bookService = bookService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if(loan == null)
            {
                return base.BadRequest($"Unable to load loan with Id '{id}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
            {
                return base.BadRequest($"Unable to load loan with Id '{id}'.");
            }

            await _loanService.UpdateReturnDateAsync(loan.Id);

            await _bookService.UpdateAvailability(loan.Book.Id, true);

            return Redirect("./Index");
        }
    }
}
