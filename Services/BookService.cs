using MongoDB.Driver;
using MongoTutorialDemo.DatabaseContext;
using MongoTutorialDemo.Enums;
using MongoTutorialDemo.ExternalAPIs;
using MongoTutorialDemo.Models;
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

        //private async Task<List<Book>> GetRadomBooks(int count)
        //{
        //    var books = new List<Book>();
        //    var images = await GetRandomImageUrlList(count);
        //    var random = new Random();
        //    for (int i = 0; i < count; i++)
        //    {
        //        books.Add(new Book
        //        {
        //            BookName = GetRandomString(random.Next(20, 50)),
        //            Price = GetRandomNumber(10, 20, random),
        //            Author = images[i].Author,
        //            BookCoverUrl = images[i].DownloadUrl,
        //            Categories = GetRandomCategories(random)
        //        });
        //    }

        //    return books;
        //}

        

        //private string GetRandomString(int length)
        //{
        //    var random = new Random();
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789 ";

        //    var rawStr= new string(Enumerable.Repeat(chars, length)
        //            .Select(s => s[random.Next(s.Length)]).ToArray()).ToLower();

        //    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rawStr);
        //}

        //private decimal GetRandomNumber(double minimum, double maximum, Random random/* = null*/)
        //{
        //    //random ??= new Random();
        //    var num= random.NextDouble() * (maximum - minimum) + minimum;
        //    return (decimal)Math.Ceiling(num * 100) / 100;
        //}

        //private async Task<List<ImageItem>> GetRandomImageUrlList(int count)
        //{
        //    var client = new HttpClient();
        //    var url = $"https://picsum.photos/v2/list?limit={count}";
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    response.EnsureSuccessStatusCode();
        //    var result = await response.Content.ReadAsStringAsync();
        //    var objs = JsonConvert.DeserializeObject<List<ImageItem>>(result);
        //    return objs;
        //}
    }
}