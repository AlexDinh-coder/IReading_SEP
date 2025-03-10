using BusinessObject;
using BusinessObject.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace LBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController
    {
        private readonly IInformationRepository _informationRepository;
        public InformationController(IInformationRepository informationRepository)
        {
            _informationRepository = informationRepository;
        }

        [Route("BasicKnowledge")]
        [HttpPost]
        public async Task<ReponderModel<BasicKnowledge>> BasicKnowledge(RequestModel model)
        {
            var result = await _informationRepository.BasicKnowledge(model.Data);
            return result;
        }

        [Route("KnowledgeDetail")]
        [HttpGet]
        public async Task<ReponderModel<BasicKnowledge>> KnowledgeDetail(int id)
        {
            var result = await _informationRepository.KnowledgeDetail(id);
            return result;
        }


    }
}
