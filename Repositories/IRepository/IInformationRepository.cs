using BusinessObject.BaseModel;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IInformationRepository
    {
        Task<ReponderModel<BasicKnowledge>> BasicKnowledge(string search);
        Task<ReponderModel<BasicKnowledge>> KnowledgeDetail(int id);
    }
}
