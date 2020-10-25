using MongoDB.Driver;
using MongoTutorialDemo.DatabaseContext;
using MongoTutorialDemo.Extensions;
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

        public async Task<bool> BulkInsert(DateTime? date = null)
        {
            var books = await BookAPIs.GetBooks(date);
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

        public PagingResult PageIndexingItems(PagingRequest request, BookFilter filter)
        {
            if (request.CurrentPage == null || request.ItemsPerPage == null)
            {
                var (item1, count1) = FilteredMongoCollection(filter);
                return new PagingResult
                {
                    Items = item1,
                    MaxItemCount = count1
                };
            }
            var skipItems = (request.CurrentPage.Value - 1) * request.ItemsPerPage.Value;

            var (items, count) = FilteredMongoCollection(filter, skipItems, request.ItemsPerPage.Value);

            //var maxPage = (int)Math.Ceiling((double)count / request.ItemsPerPage.Value);

            return new PagingResult
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                MaxItemCount = count,
                Items = items
            };
        }

        private (IEnumerable<Book> Items, int Count) FilteredMongoCollection(BookFilter filter, int? skip = null, int? take = null)
        {
            var items = _books.AsQueryable()
                        .WhereIf(filter.BookName.HasValue(), x=>x.BookName.Contains(filter.BookName))
                        .WhereIf(filter.Rate!=null, x => x.Rate >= filter.Rate)
                        ;




            var count = items.Count<Book>();

            if (skip.HasValue)
                items = items.Skip(skip.Value);

            if (take.HasValue)
                items = items.Take(take.Value);

            return (items, count);
        }

        private bool ValidateFilter(string value, string key)
        {
            if (string.IsNullOrEmpty(key))
                return true;

            return value.Contains(key);
        }
    }
}