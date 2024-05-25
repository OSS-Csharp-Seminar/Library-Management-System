using AutoMapper;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models.Book;
using N_Tier.Core.Entities;
using N_Tier.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IMapper _mapper;
        
        public BookService(IBookRepository bookRepository, IWorkRepository workRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _workRepository = workRepository;
            _mapper = mapper;
        }

        public async Task<CreateBookResponseModel> CreateAsync(CreateBookModel createBookModel)
        {
            var work = await _workRepository.GetById(createBookModel.WorkId);
            var book = _mapper.Map<Book>(createBookModel);

            book.Work = work;

            return new CreateBookResponseModel
            {
                Id = (await _bookRepository.AddAsync(book)).Id
            };
        }

        public async Task<IEnumerable<BookResponseModel>> GetAllAsync()
        {
            var books = await _bookRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<BookResponseModel>>(books);
        }

        public async Task<IEnumerable<BookResponseModel>> GetAllAvailableAsync()
        {
            var books = await _bookRepository.GetAll().Where(b => b.Availability == true).ToListAsync();

            return _mapper.Map<IEnumerable<BookResponseModel>>(books);
        }

        public async Task<BookResponseModel> GetById(Guid id)
        {
            var book = await _bookRepository.GetById(id);
            
            return _mapper.Map<BookResponseModel>(book);    
        }

        public async Task<UpdateBookResponseModel> UpdateAsync(Guid id, BookResponseModel bookResponseModel)
        {
            var book = await _bookRepository.GetById(id);

            book.Status = bookResponseModel.Status;
            book.ReleaseDate = bookResponseModel.ReleaseDate; 
            book.Availability = bookResponseModel.Availability;   
            book.Work = bookResponseModel.Work;

            return new UpdateBookResponseModel
            {
                Id = (await _bookRepository.UpdateAsync(book)).Id
            };
        }

        public async Task<bool> UpdateAvailability(Guid id, bool available)
        {
            var book = await _bookRepository.GetById(id);
            
            book.Availability = available;

            await _bookRepository.UpdateAsync(book);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            await _bookRepository.DeleteAsync(book);

            return true;
        }
    }
}
