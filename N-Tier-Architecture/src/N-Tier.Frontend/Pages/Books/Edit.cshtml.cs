using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models.Book;
using N_Tier.Application.Models.Work;
using N_Tier.Application.Services;
using N_Tier.Core.Entities;

namespace N_Tier.Frontend.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IWorkService _workService;
        private readonly IMapper _mapper;

        public IEnumerable<WorkResponseModel> Works { get; set; } = new List<WorkResponseModel>();

        public EditModel(IBookService bookService, IWorkService workService, IMapper mapper)
        {
            _bookService = bookService;
            _workService = workService;
            _mapper = mapper;
        }

        [BindProperty]
        public UpdateBookModel updateBookModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var book = await _bookService.GetById(id);
            Works = await _workService.GetAllAsync();

            if (book == null)
            {
                return base.BadRequest($"Unable to load book with ID '{id}'.");
            }

            updateBookModel = new UpdateBookModel
            {
                Status = book.Status,
                Availability = book.Availability,
                ReleaseDate = book.ReleaseDate,
                WorkId = book.Work.Id
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var book = await _bookService.GetById(id);
            if (book == null)
            {
                return base.BadRequest($"Unable to load book with ID '{id}'.");
            }

            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(updateBookModel.Status != book.Status)
            {
                book.Status = updateBookModel.Status;
            }

            if(updateBookModel.Availability != book.Availability)
            {
                book.Availability = updateBookModel.Availability;
            }

            if(updateBookModel.ReleaseDate != book.ReleaseDate)
            {
                book.ReleaseDate = updateBookModel.ReleaseDate;
            }

            if(updateBookModel.WorkId != book.Work.Id)
            {
                var work = await _workService.GetById(updateBookModel.WorkId);
                book.Work = _mapper.Map<Work>(work);
            }

            await _bookService.UpdateAsync(id, book);

            return RedirectToPage("./Index");
        }
    }
}
