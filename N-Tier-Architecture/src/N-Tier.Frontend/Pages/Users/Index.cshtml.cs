using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models;
using N_Tier.Application.Services;
using N_Tier.Application.Services.Impl;
using N_Tier.Core.Entities.Identity;

namespace N_Tier.Frontend.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        public IndexModel(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
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

            Users = _userService.Search(Users, searchString);

            Users = _userService.Sort(Users, sortString);

            var usersSize = Users.Count();

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
