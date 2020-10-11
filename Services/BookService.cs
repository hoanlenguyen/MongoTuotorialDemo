using MongoDB.Driver;
using MongoTutorialDemo.DatabaseContext;
using MongoTutorialDemo.Enums;
using MongoTutorialDemo.ExternalAPIs;
using MongoTutorialDemo.Models;
using MongoTutorialDemo.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoTutorialDemo.Services
{
    public class BookService
    {
        private const string _collectionName = "Books";

        List<string> _genres => GenreList.Names;

        private readonly IMongoCollection<Book> _books;

        public BookService(MongoDbContext mongoDb)
        {
            _books = mongoDb.Database.GetCollection<Book>(_collectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public IEnumerable<Book> GetByCategory(string genre) =>
           _books.Find<Book>(book => book.MainGenre == genre).ToEnumerable();

        public IEnumerable<Book> FindByNamekey(string key) =>
           _books.AsQueryable().Where(x => x.BookName.Contains(key));

        public Book Create(Book book)
        {
            book.Id = null;
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);

        public async Task<bool> BulkInsert()
        {
            var books = await BookAPIs.GetBooks();
            _books.InsertMany(books);
            return true;
        }

        public bool BulkDelete()
        {
            _books.DeleteMany(x => true);
            return true;
        }

        public bool BulkUpdate(string oldName, string newName)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Author, oldName);
            var update = Builders<Book>.Update.Set(x => x.Author, newName);
            _books.UpdateMany(filter, update);
            return true;
        }

        public PagingResult PageIndexingItems(PagingRequest request)
        {
            var items = _books.Find(book => true);
            //if(!string.IsNullOrEmpty(request.OrderBy))
            //    items=items.SortBy(x=>typeof(Book).GetField(request.OrderBy));
            var count = items.Count();
            if (request.CurrentPage == null || request.ItemsPerPage == null)
                return new PagingResult { Items = items.ToEnumerable() };

            var maxPage = Math.Ceiling((double)count / request.ItemsPerPage.Value);
            var result = new PagingResult()
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                MaxItemCount = count
            };

            if (request.CurrentPage <= maxPage)
            {
                var skipItems = (request.CurrentPage-1) * request.ItemsPerPage;
                result.Items = items.Skip(skipItems).Limit(request.ItemsPerPage).ToEnumerable();
            }

            return result;
        }
    }
}