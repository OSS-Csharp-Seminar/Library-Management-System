using AutoMapper;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models.Work;
using N_Tier.Core.Entities;
using N_Tier.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Services.Impl
{
    public class WorkService : IWorkService
    {
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workRepository;
        private readonly IAuthorRepository _authorRepository;

        public WorkService(IWorkRepository workRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _workRepository = workRepository;
            _authorRepository = authorRepository;
        }

        public async Task<CreateWorkResponseModel> CreateAsync(CreateWorkModel createWorkModel)
        {
            var author = await _authorRepository.GetById(createWorkModel.AuthorId);
            var work = _mapper.Map<Work>(createWorkModel);

            work.Author = author;

            return new CreateWorkResponseModel
            {
                Id = (await _workRepository.AddAsync(work)).Id
            };
        }

        public async Task<IEnumerable<WorkResponseModel>> GetAllAsync()
        {
            var works = await _workRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<WorkResponseModel>>(works);
        }

        public async Task<WorkResponseModel> GetById(Guid id)
        {
            var work = await _workRepository.GetById(id);

            return _mapper.Map<WorkResponseModel>(work);
        }

        public async Task<UpdateWorkResponseModel> UpdateAsync(Guid id, WorkResponseModel workResponseModel)
        {
            var work = await _workRepository.GetById(id);

            work.Title = workResponseModel.Title;
            work.Description = workResponseModel.Description;
            work.Genre = workResponseModel.Genre;
            work.Author = workResponseModel.Author;

            return new UpdateWorkResponseModel
            {
                Id = (await _workRepository.UpdateAsync(work)).Id
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var work = await _workRepository.GetById(id);

            await _workRepository.DeleteAsync(work);

            return true;
        }
    }
}
