using AutoMapper;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models.Book;
using N_Tier.Core.Entities;
using N_Tier.Core.Enums;
using N_Tier.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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
            var books = await _bookRepository.GetAll().Where(b => b.Availability == true && b.Status == Status.Available).ToListAsync();

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

            if(available == true)
            {
                book.Status = Status.Available;
            }
            if (available == false)
            {
                book.Status = Status.InUse;
            }

            await _bookRepository.UpdateAsync(book);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            await _bookRepository.DeleteAsync(book);

            return true;
        }

        public IEnumerable<BookResponseModel> Search(IEnumerable<BookResponseModel> books, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(item => item.Status.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Availability.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Work.Title.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                            .ToList();
            }

            return books;
        }

        public IEnumerable<BookResponseModel> Filter(IEnumerable<BookResponseModel> books, string filterString, DateOnly? filterDateStart, DateOnly? filterDateEnd)
        {
            if (filterString != "none")
            {
                books = books.Where(item => item.Status.ToString() == filterString);
            }

            if (filterDateStart.HasValue)
            {
                books = books.Where(item => item.ReleaseDate >= filterDateStart);
            }
            if (filterDateEnd.HasValue)
            {
                books = books.Where(item => item.ReleaseDate <= filterDateEnd);
            }

            return books;
        }

        public IEnumerable<BookResponseModel> Sort(IEnumerable<BookResponseModel> books, string sortString)
        {
            switch (sortString)
            {
                case "TitleDesc":
                    books = books.OrderByDescending(item => item.Work.Title).ToList(); break;
                case "StatusAsc":
                    books = books.OrderBy(item => item.Status).ToList(); break;
                case "StatusDesc":
                    books = books.OrderByDescending(item => item.Status).ToList(); break;
                case "ReleaseDateAsc":
                    books = books.OrderBy(item => item.ReleaseDate).ToList(); break;
                case "ReleaseDateDesc":
                    books = books.OrderByDescending(item => item.ReleaseDate).ToList(); break;
                default:
                    books = books.OrderBy(item => item.Work.Title).ToList(); break;
            }

            return books;
        }
    }
}
