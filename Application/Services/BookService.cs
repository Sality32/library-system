using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IUserRepository userRepository;
        private readonly IBorrowedRequestRepository borrowedRequestRepository;
        private readonly IBorrowedBookRepository borrowedBookRepository;
        private readonly IRequestAddCollectionRepository requestAddCollectionRepository;
        private readonly GutendexApiService rapidapiService;
        private readonly RedisCacheService redisCacheService;

        public BookService(
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IBorrowedRequestRepository borrowedRequestRepository,
            IBorrowedBookRepository borrowedBookRepository,
            IRequestAddCollectionRepository requestAddCollectionRepository,
            GutendexApiService gutendexApi,
            RedisCacheService redisCacheService)
        {
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
            this.rapidapiService = gutendexApi;
            this.redisCacheService = redisCacheService;
            this.borrowedRequestRepository = borrowedRequestRepository;
            this.borrowedBookRepository = borrowedBookRepository;
            this.requestAddCollectionRepository = requestAddCollectionRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await this.bookRepository.GetAllAsync();
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
            Book book = await this.bookRepository.UpdateAsync(new Book
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
            var book = await this.bookRepository.GetAsync(Guid.Parse(id));

            return book;
        }

        public async Task<string> AddBorrowedRequestAsync(CreateBorrowedRequestDto createBorrowedRequestDto)
        {
            
            var book = await this.bookRepository.GetAsync(createBorrowedRequestDto.BookId);
            var user = await userRepository.GetById(createBorrowedRequestDto.UserId);

            if (book == null || user == null) 
            {
                return "UserId or BookId Not found";
            }

            var borrowedRequest = await this.borrowedRequestRepository.AddAsync(new BorrowedRequest
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
            var borrowedRequest = await this.borrowedRequestRepository.GetAsync(approvalBorrowedRequest.BorrowedRequestId);

            if (borrowedRequest == null) { return new { code=400, message= "Request Borrowed Not Found" }; }

            if (approvalBorrowedRequest.Status != "Approve" && approvalBorrowedRequest.Status != "Decline") { return new { code=400, message = "Invalid Status Approval" }; }

            borrowedRequest.Status = approvalBorrowedRequest.Status;
            await this.borrowedRequestRepository.UpdateAsync(borrowedRequest);

            if (approvalBorrowedRequest.Status == "Approve")
            {
                var book = await this.bookRepository.GetAsync(borrowedRequest.BookId);
                await this.bookRepository.UpdateAsync(new Book
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

            var borrowedBook = await this.borrowedBookRepository.AddAsync(new BorrowedBook { BorrowedRequestId = borrowedRequest.Id });
           
            return new { code =201, message = $"Request Borrowed with number {borrowedBook.BorrowedRequestId} was {approvalBorrowedRequest.Status}" };
        }

        public async Task<string> RequestAddNewBookCollectionFromAPI(string emailUser)
        {

            var lastPageApi = await this.redisCacheService.GetCacheValueAsync("lastPageApi");
            var nextPage = lastPageApi != null ? int.Parse(lastPageApi) + 1 : 1;
            await this.redisCacheService.SetCacheValueAsync("lastPageApi", $"{nextPage}");
            var body = await rapidapiService.GetBooksByYear(nextPage);
            var key = $"{emailUser}-{nextPage}";
            await this.redisCacheService.SetCacheValueAsync(key, JsonConvert.SerializeObject(body.Results));

            var requestAddCollection = await this.requestAddCollectionRepository.AddAsync(
                new RequestAddCollection
                {
                    DetailBooksKey = key,
                    Status = "Requested",
                    RequestDate = DateTime.Now,
                });
            return $"Request Add new collection success with Id = {requestAddCollection.Id}"; 

        }

        public async Task<object> GetListRequestNewCollection()
        {
            var result = await this.requestAddCollectionRepository.GetAllAsync();

            return result;

        }

        public async Task<object> GetRequestNewCollection(Guid id)
        {
            var requestNewCollection = await this.requestAddCollectionRepository.GetAsync(id);

            var temp = await this.redisCacheService.GetCacheValueAsync(requestNewCollection.DetailBooksKey);
            var tempConvert = JsonConvert.DeserializeObject<List<ApiExternalResponseDto.ResultObject>>(temp);
            List<CreateBookDto> bookObjects = tempConvert.Select(resultObj => new CreateBookDto
            {
                Title = resultObj.Title,
                Author = string.Join(", ", resultObj.Authors.Select(a => a.Name)),
                Description = string.Join(", ", resultObj.Descriptions),
                Year = "-",
                Publisher = "-",
                Category = string.Join(", ", resultObj.Categories),
                ImageUrl = resultObj.Format.ImageUrl,
                Quantity = 10,
            }).ToList();
            var result = new RequestAddCollectionDto
            {
                Id = id,
                DetailBooksKey = requestNewCollection.DetailBooksKey,
                RequestDate = requestNewCollection.RequestDate,
                BookObjects = bookObjects
            };

            return result;

        }
    }
}