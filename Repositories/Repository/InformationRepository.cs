using BusinessObject.BaseModel;
using BusinessObject;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata;

namespace Repositories.Repository
{
    public class InformationRepository : IInformationRepository
    {
        private readonly LBSDbContext _lBSDbContext;
        private readonly LBSMongoDBContext _mongoContext;
        public InformationRepository(LBSDbContext lBSDbContext, LBSMongoDBContext mongoContext)
        {
            _lBSDbContext = lBSDbContext;
            _mongoContext = mongoContext;
        }
        public async Task<ReponderModel<BasicKnowledge>> BasicKnowledge(string search)
        {
            var result = new ReponderModel<BasicKnowledge>();
            var iquery = _lBSDbContext.BasicKnowledges.AsQueryable();
            if (!string.IsNullOrEmpty(search)) iquery = iquery.Where(c => c.Title.Contains(search));
            result.DataList = await iquery.ToListAsync();
            result.IsSussess = true;
            return result;
        }


        public async Task<ReponderModel<BasicKnowledge>> KnowledgeDetail(int id)
        {
            var result = new ReponderModel<BasicKnowledge>();
            var data = await _lBSDbContext.BasicKnowledges.FirstOrDefaultAsync(c => c.Id == id);
            if(data == null)
            {
                result.Message = "Data không hợp lệ";
                return result;
            }
            result.Data = data;
            return result;
        }


    }
}
