using MongoDB.Bson;
using MongoDB.Driver;
using MongoTutorialDemo.DatabaseContext;
using MongoTutorialDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MongoTutorialDemo.Services
{
    public class BookService
    {
        const string collectionName = "Books";
        private readonly IMongoCollection<Book> _books;
        
        public BookService(MongoDbContext mongoDb)
        {
            _books = mongoDb.Database.GetCollection<Book>(collectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public IEnumerable<Book> GetByCategory(string category) =>
           _books.Find<Book>(book => book.Category == category).ToEnumerable();

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

        public bool BulkInsert()
        {
            var books = new Collection<Book>();
            for (int i = 0; i < 50; i++)
            {
                books.Add(new Book { Author = "Hoan", BookName = RandomString(10), Category="Novel" , Price=(decimal)GetRandomNumber(1,20)});
            }
            _books.InsertMany(books);
            return true;
        }

        public bool BulkDelete()
        {
            _books.DeleteMany(x=>true);
            return true;
        }

        public bool BulkUpdate(string oldName, string newName)
        {
            var filter = Builders<Book>.Filter.Eq(x => x.Author, oldName);
            var update = Builders<Book>.Update.Set(x => x.Author, newName);
            _books.UpdateMany(filter, update);
            return true;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            var num= random.NextDouble() * (maximum - minimum) + minimum;
            return Math.Ceiling(num * 100) / 100;
        }


    }
}