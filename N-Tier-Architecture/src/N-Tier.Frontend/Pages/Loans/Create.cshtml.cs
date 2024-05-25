using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models.Book;
using N_Tier.Application.Models.Loan;
using N_Tier.Application.Services;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;

namespace N_Tier.Frontend.Pages.Loans
{
    public class CreateModel : PageModel
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public string CustomerId { get; set; }
        public string LibrarianId { get; set; }

        public IEnumerable<BookResponseModel> Books { get; set; } = new List<BookResponseModel>();

        public CreateModel(ILoanService loanService, IBookService bookService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _loanService = loanService;
            _bookService = bookService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Books = await _bookService.GetAllAvailableAsync();

            var customer = await _userManager.GetUserAsync(User);

            var librarian = await _userService.GetRandomLibrarianAsync();
            if(librarian == null)
            {
                return Page();
            }

            CustomerId = customer.Id;
            LibrarianId = librarian.Id;

            return Page();
        }


        [BindProperty]
        public CreateLoanModel Loan { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(string customerId, string librarianId)
        {
            Loan.Customer = await _userManager.FindByIdAsync(customerId);
            Loan.Librarian = await _userManager.FindByIdAsync(librarianId);
            Loan.LoanDate = DateTime.Now;
            Loan.DueDate = Loan.LoanDate.AddDays(30);
            Loan.ReturnDate = null;
            Loan.Fine = 0;

            await _bookService.UpdateAvailability(Loan.BookId, false);

            try
            {
                await _loanService.CreateAsync(Loan);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Redirect("./Index");
        }
    }
}
