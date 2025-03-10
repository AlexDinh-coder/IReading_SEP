using BusinessObject;
using BusinessObject.BaseModel;
using LBSWeb.API;
using LBSWeb.Common;

namespace LBSWeb.Service.Information
{
    public class InformationService : IInformationService
    {
        public static WebAPICaller _api;
        public InformationService(WebAPICaller api)
        {
            _api = api;
        }

        public async Task<ReponderModel<BasicKnowledge>> BasicKnowledge(string search)
        {
            var res = new ReponderModel<BasicKnowledge>();
            try
            {
                var model = new RequestModel
                {
                    Data = search,
                };
                string url = PathUrl.INFO_LIST_BASICKNOWLEDGE;
                res = await _api.Post<ReponderModel<BasicKnowledge>>(url, model);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }

        public async Task<ReponderModel<BasicKnowledge>> KnowledgeDetail(int id)
        {
            var res = new ReponderModel<BasicKnowledge>();
            try
            {
                string url = PathUrl.INFO_DETAIL_BASICKNOWLEDGE;
                var param = new Dictionary<string, string>();
                param.Add("id", id.ToString());
                res = await _api.Get<ReponderModel<BasicKnowledge>>(url, param);

            }
            catch (Exception ex)
            {
                res.Message = "Lỗi gọi api!";
            }
            return res;
        }
    }
}
