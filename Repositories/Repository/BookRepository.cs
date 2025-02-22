using BusinessObject;
using BusinessObject.BaseModel;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LBSMongoDBContext _mongoContext;
        private readonly LBSDbContext _lBSDbContext;
        private ImageManager _imageManager;
        public BookRepository(LBSMongoDBContext mongoContext, LBSDbContext lBSDbContext, ImageManager imageManager)
        {
            _mongoContext = mongoContext;
            _lBSDbContext = lBSDbContext;
            _imageManager = imageManager;
        }

        public async Task GetBookImages()
        {
            var bookImage = await _mongoContext.BookImages.Find(_ => true).ToListAsync();
            var s = await _mongoContext.BookChapters.Find(_ => true).ToListAsync();

            throw new NotImplementedException();
        }

        public async Task<ReponderModel<string>> UpdateCategory(Category model)
        {
            var result = new ReponderModel<string>();
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
            {
                result.Message = "Hãy nhập thể loại";
                return result;
            }
            var cate = await _lBSDbContext.Categories.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (cate == null) _lBSDbContext.Categories.Add(model);
            else
            {
                cate.Name = model.Name;
            }

            await _lBSDbContext.SaveChangesAsync();
            result.IsSussess = true;
            result.Message = "Cập nhật thành công";
            return result;
        }



        public async Task<ReponderModel<Category>> GetCategories()
        {
            var result = new ReponderModel<Category>();

            result.DataList = await _lBSDbContext.Categories.ToListAsync();
            result.IsSussess = true;
            return result;
        }

        public async Task<ReponderModel<string>> DeleteCategory(int id)
        {
            var result = new ReponderModel<string>();
            var cate = await _lBSDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (cate == null)
            {
                result.Message = "Không tồn tại dữ liệu";
                return result;
            }
            _lBSDbContext.Categories.Remove(cate);
            await _lBSDbContext.SaveChangesAsync();

            result.IsSussess = true;
            result.Message = "Xóa thành công";
            return result;
        }

        public Task<ReponderModel<string>> UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<ReponderModel<string>> CreateBook(BookModel bookModel)
        {
            var result = new ReponderModel<string>();

            if (bookModel == null)
            {
                result.Message = "Dữ liệu không hợp lệ";
                return result;
            }

            if (string.IsNullOrEmpty(bookModel.Name))
            {
                result.Message = "Nhập tên truyện";
                return result;
            }

            if (string.IsNullOrEmpty(bookModel.Summary))
            {
                result.Message = "Nhập tóm tắt";
                return result;
            }

            //var memoryStream = new MemoryStream();
            //bookModel.FileUpload.CopyTo(memoryStream);
            //var base64Str = Convert.ToBase64String(memoryStream.ToArray());

            var book = new Book
            {
                Name = bookModel.Name,
                Summary = bookModel.Summary,
                CategoryId = bookModel.CategoryId,
                AgeLimitType = bookModel.AgeLimitType,
                BookType = bookModel.BookType,
                Price = bookModel.Price,
                CreateBy = bookModel.CreateBy,
                Status = BookStatus.PendingPublication,
                UserId = bookModel.UserId,
                //Poster = response.Data.Link,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };

            if (!string.IsNullOrEmpty(bookModel.Poster))
            {
                bookModel.Poster = bookModel.Poster.Split("base64,")[1];
                var response = await _imageManager.UploadImage(bookModel.Poster);
                if (!response.Success)
                {
                    result.Message = "Lỗi upload ảnh";
                    return result;
                }
                book.Poster = response.Data.Link;
            }
            else bookModel.Poster = "https://i.imgur.com/KHD58yX.png";

            _lBSDbContext.Books.Add(book);
            try
            {
                await _lBSDbContext.SaveChangesAsync();
                result.Message = "Cập nhật thành công";
                result.IsSussess = true;
            }
            catch (Exception ex)
            {
                result.Message = "Lỗi server";
            }
            return result;
        }

        public async Task<ReponderModel<BookViewModel>> GetAllBookByUser(string userName)
        {
            var result = new ReponderModel<BookViewModel>();

            var listBook = await _lBSDbContext.Books.Where(c => c.CreateBy == userName).ToListAsync();

            result.DataList = new List<BookViewModel>();
            foreach (var item in listBook)
            {
                var bookChapter = await GetNewChapterPulished(item);
                bookChapter.Id = item.Id;
                bookChapter.Name = item.Name;
                bookChapter.Poster = item.Poster;
                result.DataList.Add(bookChapter);
            }

            result.IsSussess = true;

            return result;
        }

        private async Task<BookViewModel> GetNewChapterPulished(Book book)
        {
            var filter = Builders<BookChapter>.Filter.Eq(c => c.BookId, book.Id);
            var sort = Builders<BookChapter>.Sort.Descending(x => x.ChaperId);
            var listBookChapter = await _mongoContext.BookChapters.Find(filter).Sort(sort).ToListAsync();
            var lastedBookChapter = listBookChapter.FirstOrDefault();

            var bookViewModel = new BookViewModel
            {
                BookStatus = BookStatusName.ListBookStatus[(int)book.Status],
                NewPulished = lastedBookChapter != null && !string.IsNullOrEmpty(lastedBookChapter.ChapterName) ? lastedBookChapter.ChapterName : string.Empty,
                NewPulishedDateTime = lastedBookChapter != null ? lastedBookChapter.ModifyDate.ToString() : string.Empty,
            };

            return bookViewModel;
        }

        public async Task<ReponderModel<Book>> GetBook(int id)
        {
            var result = new ReponderModel<Book>();

            var book = await _lBSDbContext.Books.FirstOrDefaultAsync(c => c.Id == id);

            if (book == null)
            {
                result.Message = "Data không hợp lệ";
                return result;
            }

            result.Data = book;
            result.IsSussess = true;
            return result;
        }

    }
}
