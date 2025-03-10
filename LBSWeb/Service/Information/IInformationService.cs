using BusinessObject.BaseModel;
using BusinessObject;

namespace LBSWeb.Service.Information
{
    public interface IInformationService
    {
        Task<ReponderModel<BasicKnowledge>> BasicKnowledge(string search);
        Task<ReponderModel<BasicKnowledge>> KnowledgeDetail(int id);
    }
}
