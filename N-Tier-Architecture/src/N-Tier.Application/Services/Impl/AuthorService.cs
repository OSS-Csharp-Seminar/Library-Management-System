using AutoMapper;
using Microsoft.AspNetCore.Identity;
using N_Tier.Application.Models.Author;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using N_Tier.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Services.Impl
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthorService(IMapper mapper, IAuthorRepository authorRepository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<CreateAuthorResponseModel> CreateAsync(CreateAuthorModel createAuthorModel)
        {
            var author = _mapper.Map<Author>(createAuthorModel);

            return new CreateAuthorResponseModel
            {
                Id = (await _authorRepository.AddAsync(author)).Id
            };
        }

        public async Task<IEnumerable<AuthorResponseModel>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAll();

            return _mapper.Map<IEnumerable<AuthorResponseModel>>(authors);
        }

        public async Task<AuthorResponseModel> GetById(Guid id)
        {
            var author = await _authorRepository.GetById(id);

            return _mapper.Map<AuthorResponseModel>(author);
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetById(id);

            return author;
        }

        public async Task<UpdateAuthorReponseModel> UpdateAsync(Guid id, AuthorResponseModel updateAuthorModel)
        {
            var author = await _authorRepository.GetFirstAsync(x => x.Id == id);

            author.FirstName = updateAuthorModel.FirstName;
            author.LastName = updateAuthorModel.LastName;
            author.DateOfBirth = updateAuthorModel.DateOfBirth;
            author.About = updateAuthorModel.About;

            return new UpdateAuthorReponseModel
            {
                Id = (await _authorRepository.UpdateAsync(author)).Id
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var author = await _authorRepository.GetFirstAsync(x =>x.Id == id);
            await _authorRepository.DeleteAsync(author);
            return true;
        }
    }
}
