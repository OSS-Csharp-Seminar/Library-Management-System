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

        public IEnumerable<AuthorResponseModel> Search(IEnumerable<AuthorResponseModel> authors, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(item => item.FirstName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                        || item.LastName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                        || item.About.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return authors;
        }

        public IEnumerable<AuthorResponseModel> FilterByDate(IEnumerable<AuthorResponseModel> authors, DateOnly? filterDateStart, DateOnly? filterDateEnd)
        {
            if (filterDateStart.HasValue)
            {
                authors = authors.Where(item => item.DateOfBirth >= filterDateStart);
            }
            if (filterDateEnd.HasValue)
            {
                authors = authors.Where(item => item.DateOfBirth <= filterDateEnd);
            }

            return authors;    
        }

        public IEnumerable<AuthorResponseModel> Sort(IEnumerable<AuthorResponseModel> authors, string sortString)
        {
            switch (sortString)
            {
                case "FirstNameDesc":
                    authors = authors.OrderByDescending(item => item.FirstName).ToList(); break;
                case "LastNameAsc":
                    authors = authors.OrderBy(item => item.LastName).ToList(); break;
                case "LastNameDesc":
                    authors = authors.OrderByDescending(item => item.LastName).ToList(); break;
                case "DateOfBirthAsc":
                    authors = authors.OrderBy(item => item.DateOfBirth).ToList(); break;
                case "DateOfBirthDesc":
                    authors = authors.OrderByDescending(item => item.DateOfBirth).ToList(); break;
                default:
                    authors = authors.OrderBy(item => item.FirstName).ToList(); break;
            }

            return authors;
        }
    }
}
