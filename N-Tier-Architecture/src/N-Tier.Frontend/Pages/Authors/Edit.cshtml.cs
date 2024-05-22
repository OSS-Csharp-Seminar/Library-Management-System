using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using N_Tier.Application.MappingProfiles;
using N_Tier.Application.Models.Author;
using N_Tier.Application.Services;

namespace N_Tier.Frontend.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public EditModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [BindProperty]
        public UpdateAuthorModel Author {  get; set; } =  default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var author = await _authorService.GetById(id);
            if (author == null) 
            {
                return base.BadRequest($"Unable to load author with id '{id}'.");
            }

            Author = new UpdateAuthorModel
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                About = author.About
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
            {
                return base.BadRequest($"Unable to load author with id '{id}'.");
            }

            if(!ModelState.IsValid)
            {
                return Page();
            }
            
            var firstname = author.FirstName;
            if(Author.FirstName != firstname)
            {
                author.FirstName = Author.FirstName;
            }
            
            var lastname = author.LastName;
            if(Author.LastName != lastname)
            {
                author.LastName = Author.LastName;
            }

            var dateOfBirth = author.DateOfBirth;
            if(Author.DateOfBirth  != dateOfBirth)
            {
                author.DateOfBirth = Author.DateOfBirth;
            }

            var about = author.About;
            if(Author.About != about)
            {
                author.About = Author.About;   
            }

            await _authorService.UpdateAsync(id, author);

            return Redirect("./Index");
        }
    }
}
