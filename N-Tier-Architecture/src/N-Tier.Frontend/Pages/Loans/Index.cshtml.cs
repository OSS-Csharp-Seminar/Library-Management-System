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

            if (!string.IsNullOrEmpty(searchString))
            {
                Loans = Loans.Where(item => item.Book.Work.Title.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Customer.FirstName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Customer.LastName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Librarian.FirstName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Librarian.LastName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                            .ToList();
            }

            if(filterString == "LoanDate")
            {
                if(filterDateStart.HasValue)
                {
                    Loans = Loans.Where(item => item.LoanDate >=  filterDateStart);
                }
                if(filterDateEnd.HasValue)
                {
                    Loans = Loans.Where(item => item.LoanDate <= filterDateEnd);
                }
            }

            if (filterString == "DueDate")
            {
                if (filterDateStart.HasValue)
                {
                    Loans = Loans.Where(item => item.DueDate >= filterDateStart);
                }
                if (filterDateEnd.HasValue)
                {
                    Loans = Loans.Where(item => item.DueDate <= filterDateEnd);
                }
            }

            if (filterString == "ReturnDate")
            {
                if (filterDateStart.HasValue)
                {
                    Loans = Loans.Where(item => item.ReturnDate >= filterDateStart);
                }
                if (filterDateEnd.HasValue)
                {
                    Loans = Loans.Where(item => item.ReturnDate <= filterDateEnd);
                }
            }


            switch (sortString)
            {
                case "TitleDesc":
                    Loans = Loans.OrderByDescending(item => item.Book.Work.Title).ToList(); break;
                case "LoanDateAsc":
                    Loans = Loans.OrderBy(item => item.LoanDate).ToList(); break;
                case "LoanDateDesc":
                    Loans = Loans.OrderByDescending(item => item.LoanDate).ToList(); break;
                case "DueDateAsc":
                    Loans = Loans.OrderBy(item => item.DueDate).ToList(); break;
                case "DueDateDesc":
                    Loans = Loans.OrderByDescending(item => item.DueDate).ToList(); break;
                case "ReturnDateAsc":
                    Loans = Loans.OrderBy(item => item.ReturnDate).ToList(); break;
                case "ReturnDateDesc":
                    Loans = Loans.OrderByDescending(item => item.ReturnDate).ToList(); break;
                case "FineAsc":
                    Loans = Loans.OrderBy(item => item.Fine).ToList(); break;
                case "FineDesc":
                    Loans = Loans.OrderByDescending(item => item.Fine).ToList(); break;
                default:
                    Loans = Loans.OrderBy(item => item.Book.Work.Title).ToList(); break;
            }

            int loanSize = Loans.Count();

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
