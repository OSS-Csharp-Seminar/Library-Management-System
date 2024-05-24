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

        public async Task<IActionResult> OnGetAsync(string searchString, int pageNumber, string sortString, int pageSize = 5)
        {
            ViewData["searchString"] = searchString;
            ViewData["pageSize"] = pageSize;
            ViewData["sortString"] = sortString;

            pageNumber = pageNumber == 0 ? 1 : pageNumber;

            Books = await _bookService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                Books = Books.Where(item => item.Status.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Availability.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Work.Title.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                            .ToList();
            }

            int booksSize = Books.Count();

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
