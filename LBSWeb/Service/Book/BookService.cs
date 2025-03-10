﻿using BusinessObject.BaseModel;
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

        public async Task<ReponderModel<string>> DeleteChapterBook(string id)
        {
            var res = new ReponderModel<string>();
            try
            {
                string url = PathUrl.BOOK_DELETE_CHAPTER;
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


        public async Task<ReponderModel<string>> GenerateSummary(string input)
        {
            var model = new RequestModel
            {
                Data = input
            };
            var res = new ReponderModel<string>();
            if (string.IsNullOrEmpty(input))
            {
                res.Message = "Cần nhập nội dung chương";
                return res;
            }
            try
            {
                string url = PathUrl.BOOK_GENERATE_SUMMARY_CHAPTER;
                res = await _api.Post<ReponderModel<string>>(url, model);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<DraftModel>> GetDrafts(string userName)
        {
            var res = new ReponderModel<DraftModel>();
            try
            {
                string url = PathUrl.BOOK_GET_DRAFT;
                var param = new Dictionary<string, string>();
                param.Add("userName", userName);
                res = await _api.Get<ReponderModel<DraftModel>>(url, param);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<string>> GeneratePoster(string input)
        {
            var model = new RequestModel
            {
                Data = input
            };
            var res = new ReponderModel<string>();
            if (string.IsNullOrEmpty(input))
            {
                res.Message = "Cần nhập tên truyện";
                return res;
            }
            try
            {
                string url = PathUrl.BOOK_GENERATE_POSTER_CHAPTER;
                res = await _api.Post<ReponderModel<string>>(url, model);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
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

        public async Task<ReponderModel<string>> CreateBook(BookModel model)
        {
            var res = new ReponderModel<string>();
            if (model == null)
            {
                res.Message = "Thông tin không hợp lệ!";
                return res;
            }
            try
            {
                string url = PathUrl.BOOK_CREATE;
                res = await _api.Post<ReponderModel<string>>(url, model);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<BookViewModel>> GetAllBookByUser(string userName)
        {
            var res = new ReponderModel<BookViewModel>();
            try
            {
                string url = PathUrl.BOOK_GET_BY_USER;
                var param = new Dictionary<string, string>();
                param.Add("userName", userName);
                res = await _api.Get<ReponderModel<BookViewModel>>(url, param);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }


        public async Task<ReponderModel<BusinessObject.Book>> GetBook(int id)
        {
            var res = new ReponderModel<BusinessObject.Book>();
            try
            {
                string url = PathUrl.BOOK_GET;
                var param = new Dictionary<string, string>();
                param.Add("id", id.ToString());
                res = await _api.Get<ReponderModel<BusinessObject.Book>>(url, param);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }


    }

            public async Task<ReponderModel<BookChapter>> GetBookChapter(string id)
        {
            var res = new ReponderModel<BookChapter>();
            try
            {
                string url = PathUrl.BOOK_GET_BOOK_CHAPTER;
                var param = new Dictionary<string, string>();
                param.Add("id", id);
                res = await _api.Get<ReponderModel<BookChapter>>(url, param);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<string>> UpdateBookChapter(BookChapter model)
        {
            var res = new ReponderModel<string>();
            if (model == null)
            {
                res.Message = "Thông tin không hợp lệ!";
                return res;
            }
            try
            {
                string url = PathUrl.BOOK_UPDATE_CHAPTER;
                res = await _api.Post<ReponderModel<string>>(url, model);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<ReportModel>> ShortReport()
        {
            var res = new ReponderModel<ReportModel>();
            try
            {
                string url = PathUrl.REPORT_SHORT;
                res = await _api.Get<ReponderModel<ReportModel>>(url);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<string>> DeleteChapterBook(string id)
        {
            var res = new ReponderModel<string>();
            try
            {
                string url = PathUrl.BOOK_DELETE_CHAPTER;
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
}
