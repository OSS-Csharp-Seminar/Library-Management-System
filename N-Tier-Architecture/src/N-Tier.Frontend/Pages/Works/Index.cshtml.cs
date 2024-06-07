using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models;
using N_Tier.Application.Models.Work;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Works
{
    [Authorize(Roles = "Administrator, Librarian")]
    public class IndexModel : PageModel
    {
        private readonly IWorkService _workService;
        private readonly IAuthorService _authorService;

        public IndexModel(IWorkService workService, IAuthorService authorService)
        {
            _workService = workService;
            _authorService = authorService;
        }

        public int pageIndex { get; set; } = 1;
        public bool HasPreviousPage { get; set; } = false;
        public bool HasNextPage { get; set; } = false;

        public IEnumerable<WorkResponseModel> Works { get; set; } = default!;

        public async Task<PageResult> OnGetAsync(string searchString, int pageNumber, string sortString, int pageSize = 5, string filterString = "none")
        {
            ViewData["searchString"] = searchString;
            ViewData["pageSize"] = pageSize;
            ViewData["sortString"] = sortString;
            ViewData["filterString"] = filterString;

            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            Works = await _workService.GetAllAsync();

            Works = _workService.Search(Works, searchString);

            Works = _workService.Filter(Works, filterString);

            Works = _workService.Sort(Works, sortString);

            var worksSize = Works.Count();

            Works = PaginatedList<WorkResponseModel>.Create(Works, pageNumber, pageSize);

            if(pageNumber > 1)
            {
                HasPreviousPage = true;
            }
            if((pageNumber * pageSize) < worksSize)
            {
                HasNextPage = true;
            }

            pageIndex = pageNumber;

            return Page();
        }
    }
}
