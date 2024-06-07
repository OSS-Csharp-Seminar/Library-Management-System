using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models;
using N_Tier.Application.Models.Loan;
using N_Tier.Application.Services;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;

namespace N_Tier.Frontend.Pages.Loans
{
    public class IndexModel : PageModel
    {
        public readonly ILoanService _loanService;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly IBookService _bookService;

        public ApplicationUser ApplicationUser { get; set; }
        public string UserRole { get; set; }

        public IndexModel(ILoanService loanService, UserManager<ApplicationUser> userManager, IBookService bookService)
        {
            _loanService = loanService;
            _userManager = userManager;
            _bookService = bookService;
        }

        public int pageIndex { get; set; } = 1;
        public bool HasPreviousPage { get; set; } = false;
        public bool HasNextPage { get; set; } = false;

        public IEnumerable<LoanResponseModel> Loans { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber, string sortString, DateTime? filterDateStart, DateTime? filterDateEnd, int pageSize = 5, string filterString = "none")
        {
            ViewData["searchString"] = searchString;
            ViewData["pageSize"] = pageSize;
            ViewData["sortString"] = sortString;

            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            ApplicationUser = await _userManager.GetUserAsync(User);

            var roles = await _userManager.GetRolesAsync(ApplicationUser);

            UserRole = roles.First();

            if (UserRole == "Administrator")
            {
                Loans = await _loanService.GetAllAsync();
                await _loanService.UpdateFinesAsync(Loans);
            }
            if (UserRole == "Customer")
            {
                Loans = await _loanService.GetByCustomerAsync(ApplicationUser);
                await _loanService.UpdateFinesAsync(Loans);
            }
            if( UserRole == "Librarian")
            {
                Loans = await _loanService.GetByLibrarianAsync(ApplicationUser);
                await _loanService.UpdateFinesAsync(Loans);
            }

            Loans = _loanService.Search(Loans, searchString);

            Loans = _loanService.Filter(Loans, filterDateStart, filterDateEnd, filterString);

            Loans = _loanService.Sort(Loans, sortString);

            var loanSize = Loans.Count();

            Loans = PaginatedList<LoanResponseModel>.Create(Loans, pageNumber, pageSize);

            if (pageNumber > 1)
            {
                HasPreviousPage = true;
            }
            if ((pageNumber * pageSize) < loanSize)
            {
                HasNextPage = true;
            }

            pageIndex = pageNumber;

            return Page();
        }
    }
}
