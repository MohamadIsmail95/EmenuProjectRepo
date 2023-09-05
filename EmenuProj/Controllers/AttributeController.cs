using EmenuBLL.EmenuServices;
using EmenuBLL.IEmenuServices;
using EmenuDAL.Model.Filter;
using EmenuDAL.Model.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmenuProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _iattributeService;

        public AttributeController(IAttributeService iattributeService)
        {
            _iattributeService = iattributeService;
        }


        [HttpPost("GetAllAttribute")]
        public ActionResult GetAllAttribute([FromBody] List<Filter> filters, [FromQuery] ParameterPagination parameters)
        {
            var response = _iattributeService.GetAllAttributes(filters, parameters);
            return Ok(response);
        }
    }
}
