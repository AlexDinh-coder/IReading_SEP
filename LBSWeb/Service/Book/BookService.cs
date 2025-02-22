using BusinessObject.BaseModel;
using BusinessObject;
using LBSWeb.Common;
using LBSWeb.API;

namespace LBSWeb.Service.Book
{
    public class BookService : IBookService
    {
        public static WebAPICaller _api;
        public BookService(WebAPICaller api)
        {
            _api = api;
        }
        public void GetBookImages()
        {
            throw new NotImplementedException();
        }

        public async Task<ReponderModel<Category>> GetCategories()
        {
            var res = new ReponderModel<Category>();
            try
            {
                string url = PathUrl.CATEGORY_GET_ALL;
                res = await _api.Get<ReponderModel<Category>>(url);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<string>> DeleteCategory(int id)
        {
            var res = new ReponderModel<string>();
            try
            {
                string url = PathUrl.CATEGORY_DELETE;
                var param = new Dictionary<string, string>();
                param.Add("id", id.ToString());
                res = await _api.Get<ReponderModel<string>>(url, param);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<string>> UpdateCategory(Category model)
        {
            var res = new ReponderModel<string>();
            if (model == null)
            {
                res.Message = "Thông tin không hợp lệ!";
                return res;
            }
            try
            {
                string url = PathUrl.CATEGORY_UPDATE;
                res = await _api.Post<ReponderModel<string>>(url, model);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

    }
}
