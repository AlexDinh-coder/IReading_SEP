using BusinessObject;
using BusinessObject.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace LBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository) 
        { 
            _bookRepository = bookRepository;
        }

        [Route("GetAllBookImages")]
        [HttpGet]
        public async Task<bool> GetAllBookImages()
        {
            await _bookRepository.GetBookImages();
            return true;
        }

        [Route("GetCategories")]
        [HttpGet]
        public async Task<ReponderModel<Category>> GetCategories()
        {
            var result = await _bookRepository.GetCategories();
            return result;
        }

        [Route("UpdateCategory")]
        [HttpPost]
        public async Task<ReponderModel<string>> UpdateCategory(Category model)
        {
            var result = await _bookRepository.UpdateCategory(model);
            return result;
        }

        [Route("DeleteCategory")]
        [HttpGet]
        public async Task<ReponderModel<string>> DeleteCategory(int id)
        {
            var result = await _bookRepository.DeleteCategory(id);
            return result;
        }

        [Route("CreateBook")]
        [HttpPost]
        public async Task<ReponderModel<string>> CreateBook(BookModel bookModel)
        {
            var result = await _bookRepository.CreateBook(bookModel);
            return result;
        }

        [Route("GetBook")]
        [HttpGet]
        public async Task<ReponderModel<Book>> GetBook(int id)
        {
            var result = await _bookRepository.GetBook(id);
            return result;
        }

        [Route("GetAllBookByUser")]
        [HttpGet]
        public async Task<ReponderModel<BookViewModel>> GetAllBookByUser(string userName)
        {
            var result = await _bookRepository.GetAllBookByUser(userName);
            return result;
        }

                [Route("GetBookChapter")]
        [HttpGet]
        public async Task<ReponderModel<BookChapter>> GetBookChapter(string id)
        {
            var result = await _bookRepository.GetBookChapter(id);
            return result;
        }

        [Route("UpdateBookChapter")]
        [HttpPost]
        public async Task<ReponderModel<string>> UpdateBookChapter(BookChapter bookChapter)
        {
            var result = await _bookRepository.UpdateBookChapter(bookChapter);
            return result;
        }


        [Route("GetListBookChapter")]
        [HttpGet]
        public async Task<ReponderModel<BookChapter>> GetListBookChapter(int bookId)
        {
            var result = await _bookRepository.GetListBookChapter(bookId);
            return result;
        }

        [Route("DeleteChapterBook")]
        [HttpGet]
        public async Task<ReponderModel<string>> DeleteChapterBook(string id)
        {
            var result = await _bookRepository.DeleteChapterBook(id);
            return result;
        }
    }
}
