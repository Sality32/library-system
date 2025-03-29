using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IUserRepository userRepository;
        private readonly AmazonRapidapiService rapidapiService;

        public BookService(IBookRepository bookRepository, IUserRepository userRepository, AmazonRapidapiService amazonRapidapi)
        {
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
            this.rapidapiService = amazonRapidapi;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await bookRepository.GetAllAsync();
        }

        public async Task<Book> AddBook(CreateBookDto createBookDto)
        {
            Book book = await bookRepository.AddAsync(new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Publisher = createBookDto.Publisher,
                Description = createBookDto.Description,
                Year = createBookDto.Year,
                Category = createBookDto.Category,
                ImageUrl = createBookDto.ImageUrl,
                Quantity = createBookDto.Quantity,
                BorrowedQuantity = 0,
            });

            return book;
        }

        public async Task<Book> UpdateBook(string id, UpdateBookDto updateBookDto)
        {
            Book book = await bookRepository.UpdateAsync(new Book
            {
                Id = Guid.Parse(id),
                Title = updateBookDto.Title,
                Author = updateBookDto.Author,
                Publisher = updateBookDto.Publisher,
                Description = updateBookDto.Description,
                Year = updateBookDto.Year,
                Category = updateBookDto.Category,
                ImageUrl = updateBookDto.ImageUrl,
                Quantity = updateBookDto.Quantity,
                BorrowedQuantity = updateBookDto.BorrowedQuantity
            });

            return book;

        }

        public async Task<Book> GetAsync(string id) 
        {
            var book = await bookRepository.GetAsync(Guid.Parse(id));

            return book;
        }

        public async Task<string> AddBorrowedRequestAsync(CreateBorrowedRequestDto createBorrowedRequestDto)
        {
            
            var book = await bookRepository.GetAsync(createBorrowedRequestDto.BookId);
            var user = await userRepository.GetById(createBorrowedRequestDto.UserId);

            if (book == null || user == null) 
            {
                return "UserId or BookId Not found";
            }

            var borrowedRequest = await bookRepository.AddBorrowedRequestAsync(new BorrowedRequest
            {
                BookId = book.Id,
                UserId = user.Id,
                Status = "Requested",
                RequestDate = DateTime.Now,
            });
            return $"Request Borrow with number {borrowedRequest.Id} was created";

        }

        public async Task<object> ApprovalBorrowedRequest(ApprovalBorrowedRequestDto approvalBorrowedRequest)
        {
            var borrowedRequest = await bookRepository.GetBorrowedRequestAsync(approvalBorrowedRequest.BorrowedRequestId);
            if (borrowedRequest == null) { return new { code=400, message= "Request Borrowed Not Found" }; }

            if (approvalBorrowedRequest.Status != "Approve" && approvalBorrowedRequest.Status != "Decline") { return new { code=400, message = "Invalid Status Approval" }; }

            borrowedRequest.Status = approvalBorrowedRequest.Status;
            await bookRepository.UpdateBorrowedRequestAsync(borrowedRequest);

            if (approvalBorrowedRequest.Status == "Approve")
            {
                var book = await bookRepository.GetAsync(borrowedRequest.BookId);
                await bookRepository.UpdateAsync(new Book
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    Description = book.Description,
                    Year = book.Year,
                    Category = book.Category,
                    ImageUrl = book.ImageUrl,
                    Quantity = book.Quantity,
                    BorrowedQuantity = book.BorrowedQuantity + 1,
                });
            }

            var borrowedBook = await bookRepository.AddBorrowedBookAsync(new BorrowedBook { BorrowedRequestId = borrowedRequest.Id });
           
            return new { code =201, message = $"Request Borrowed with number {borrowedBook.BorrowedRequestId} was {approvalBorrowedRequest.Status}" };
        }

        public async Task<object> RequestAddNewBookCollection(string year)
        {
            var body = await rapidapiService.GetBooksByYear(year);

            return body;

        }


    }
}