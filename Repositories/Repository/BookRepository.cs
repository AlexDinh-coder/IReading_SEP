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
        public BookRepository(LBSMongoDBContext mongoContext, LBSDbContext lBSDbContext)
        {
            _mongoContext = mongoContext;
            _lBSDbContext = lBSDbContext;
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

    }
}
