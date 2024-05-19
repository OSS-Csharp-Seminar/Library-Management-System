using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models;
using N_Tier.Core.Entities.Identity;

namespace N_Tier.Frontend.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public int pageIndex { get; set; } = 1;
        public bool HasPreviousPage { get; set; } = false;
        public bool HasNextPage { get; set; } = false;

        public IEnumerable<ApplicationUser> Users { get; set; } = default!;

        public async Task<PageResult> OnGetAsync(string searchString,  int pageNumber, string sortString, int pageSize = 5)
        {
            ViewData["searchString"] = searchString;
            ViewData["sortString"] = sortString;
            ViewData["pageSize"] = pageSize;

            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            Users = await _userManager.Users
                .ToListAsync();

            if(!string.IsNullOrEmpty(searchString))
            {
                Users = Users.Where(user => (user.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                            user.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase)   ||
                                            user.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }

            switch (sortString)
            {
                case "FirstNameDesc":
                    Users = Users.OrderByDescending(item => item.FirstName).ToList(); break;
                case "LastNameAsc":
                    Users = Users.OrderBy(item => item.LastName).ToList(); break;
                case "LastNameDesc":
                    Users = Users.OrderByDescending(item => item.LastName); break;
                case "EmailAsc":
                    Users = Users.OrderBy(item => item.Email).ToList(); break;
                case "EmailDesc":
                    Users = Users.OrderByDescending((item) => item.Email).ToList(); break;
                default:
                    Users = Users.OrderBy((item) => item.FirstName).ToList(); break; // Default sorting by FirstNameAsc
            }

            int usersSize = Users.Count();

            Users = PaginatedList<ApplicationUser>.Create(Users, pageNumber, pageSize);

            if (pageSize > 1)
            {
                HasPreviousPage = true;
            }
            if ((pageNumber * pageSize) < usersSize)
            {
                HasNextPage = true;
            }

            pageIndex = pageNumber;

            return Page();
        }
    }
}
