using MongoDB.Driver;
using MongoTutorialDemo.DatabaseContext;
using MongoTutorialDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace MongoTutorialDemo.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IMongoDbConnectionSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.CollectionName);

        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public IEnumerable<Book> GetByCategory(string category) =>
           _books.Find<Book>(book => book.Category == category).ToEnumerable();

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
    }
}