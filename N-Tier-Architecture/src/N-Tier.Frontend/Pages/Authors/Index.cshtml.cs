using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models;
using N_Tier.Application.Models.Author;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public IndexModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public int pageIndex { get; set; } = 1;
        public bool HasPreviousPage { get; set; } = false;
        public bool HasNextPage { get; set; } = false;

        [BindProperty]
        public IEnumerable<AuthorResponseModel> Authors { get; set; } = default!;

        public async Task<PageResult> OnGetAsync(string searchString, int pageNumber, DateOnly? filterDateStart, DateOnly? filterDateEnd, string sortString, int pageSize = 5)
        {
            ViewData["searchString"] = searchString;
            ViewData["pageSize"] = pageSize;
            ViewData["sortString"] = sortString;

            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            Authors = await _authorService.GetAllAsync();


            Authors = _authorService.Search(Authors, searchString);

            Authors = _authorService.FilterByDate(Authors, filterDateStart, filterDateEnd);

            Authors = _authorService.Sort(Authors, sortString);

            var authorsSize = Authors.Count();

            Authors = PaginatedList<AuthorResponseModel>.Create(Authors, pageNumber, pageSize);

            if (pageNumber > 1)
            {
                HasPreviousPage = true;
            }
            if ((pageNumber * pageSize) < authorsSize)
            {
                HasNextPage = true;
            }

            pageIndex = pageNumber;

            return Page();
        }
    }
}
