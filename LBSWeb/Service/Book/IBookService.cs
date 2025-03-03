using BusinessObject.BaseModel;
using BusinessObject;

namespace LBSWeb.Service.Book
{
    public interface IBookService
    {
        void GetBookImages();
        Task<ReponderModel<Category>> GetCategories();
        Task<ReponderModel<string>> UpdateCategory(Category model);
        Task<ReponderModel<string>> DeleteCategory(int id);
        Task<ReponderModel<string>> CreateBook(BookModel bookModel);
        Task<ReponderModel<BusinessObject.Book>> GetBook(int id);
        Task<ReponderModel<BookViewModel>> GetAllBookByUser(string userName);
    }
}
