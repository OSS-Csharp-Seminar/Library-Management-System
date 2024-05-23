using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.Models.Author;
using N_Tier.Application.Models.Work;
using N_Tier.Application.Services;
using N_Tier.Core.Entities;

namespace N_Tier.Frontend.Pages.Works
{
    public class EditModel : PageModel
    {
        private readonly IWorkService _workService;
        private readonly IAuthorService _authorService;

        public IEnumerable<AuthorResponseModel> Authors { get; set; } = new List<AuthorResponseModel>();

        public EditModel(IWorkService workService, IAuthorService authorService)
        {
            _workService = workService;
            _authorService = authorService;
        }

        [BindProperty]
        public UpdateWorkModel updateWorkModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var work = await _workService.GetById(id);
            Authors = await _authorService.GetAllAsync();

            if (work == null)
            {
                return base.BadRequest($"Unable to load work with ID '{id}'.");
            }

            updateWorkModel = new UpdateWorkModel
            {
                Title = work.Title,
                Genre = work.Genre,
                Description = work.Description,
                AuthorId = work.Author.Id
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var work = await _workService.GetById(id);

            if (work == null)
            {
                return base.BadRequest($"Unable to load work with ID '{id}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var authorId = work.Author.Id;
            if (updateWorkModel.AuthorId != authorId)
            {
                var author = await _authorService.GetByIdAsync(updateWorkModel.AuthorId);
                work.Author = author;

            }

            var title = work.Title;
            if (updateWorkModel.Title != title)
            {
                work.Title = updateWorkModel.Title;
            }

            var genre = work.Genre;
            if (updateWorkModel.Genre != genre)
            {
                work.Genre = updateWorkModel.Genre;
            }

            var description = work.Description;
            if (updateWorkModel.Description != description)
            {
                work.Description = updateWorkModel.Description;
            }

            await _workService.UpdateAsync(id, work);

            return Redirect("./Index");
        }
    }
}
