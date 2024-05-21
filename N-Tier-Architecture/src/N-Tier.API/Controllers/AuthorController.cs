using Microsoft.AspNetCore.Mvc;
using N_Tier.Application.Models;
using N_Tier.Application.Models.Author;
using N_Tier.Application.Services;

namespace N_Tier.API.Controllers
{
    public class AuthorController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateAuthorModel createAuthorModel)
        {
            return Ok(ApiResult<CreateAuthorResponseModel>.Success(await _authorService.CreateAsync(createAuthorModel)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ApiResult<IEnumerable<AuthorResponseModel>>.Success(await _authorService.GetAllAsync()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(ApiResult<AuthorResponseModel>.Success(await _authorService.GetById(id)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateAuthorModel updateAuthorModel)
        {
            return Ok(ApiResult<UpdateAuthorReponseModel>.Success(await _authorService.UpdateAsync(id, updateAuthorModel)));
        }

        [HttpDelete("id:guid")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<bool>.Success(await _authorService.DeleteAsync(id)));
        }
    }
}
