using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models;
using N_Tier.Application.Models.Book;
using N_Tier.Application.Models.Work;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IWorkService _workService;

        public IndexModel(IBookService bookService, IWorkService workService)
        {
            _bookService = bookService;
            _workService = workService;
        }

        public int pageIndex { get; set; } = 1;
        public bool HasPreviousPage { get; set; } = false;
        public bool HasNextPage { get; set; } = false;

        public IEnumerable<BookResponseModel> Books { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber, string sortString, DateOnly? filterDateStart, DateOnly? filterDateEnd, int pageSize = 5, string filterString = "none")
        {
            ViewData["searchString"] = searchString;
            ViewData["pageSize"] = pageSize;
            ViewData["sortString"] = sortString;
            ViewData["filterString"] = filterString;
            ViewData["filterDateStart"] = filterDateStart;
            ViewData["filterDateEnd"] = filterDateEnd;

            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            Books = await _bookService.GetAllAsync();

            Books = _bookService.Search(Books, searchString);

            Books = _bookService.Filter(Books, filterString, filterDateStart, filterDateEnd);

            Books = _bookService.Sort(Books, sortString);

            var booksSize = Books.Count();

            Books = PaginatedList<BookResponseModel>.Create(Books, pageNumber, pageSize);

            if (pageNumber > 1)
            {
                HasPreviousPage = true;
            }
            if ((pageNumber * pageSize) < booksSize)
            {
                HasNextPage = true;
            }

            pageIndex = pageNumber;

            return Page();
        }
    }
}
