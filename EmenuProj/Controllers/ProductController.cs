using EmenuBLL.IEmenuServices;
using EmenuDAL.Model.Binding;
using EmenuDAL.Model.Filter;
using EmenuDAL.Model.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmenuProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }   

        [HttpPost("AddProduct")]
        public ActionResult AddProduct(AddProductBinding prod)
        {
            var response=_productService.AddNewProduct(prod);
            return Ok(response);
        }
        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var response = _productService.RemoveProduct(id);
            return Ok(response);
        }


        [HttpPut("UpdateProduct/{id}")]

        public ActionResult UpdateProduct(int id,UpdateProductBinding prod)
        {
            if(id!=prod.id)
            {
                return BadRequest("This Id is invalid.");
            }

            var response=_productService.UpdateProduct(prod);
            return Ok(response);
        }

        [HttpPost("GetAllProduct")]
        public ActionResult GetAllProduct([FromBody]List<Filter> filters,[FromQuery] ParameterPagination parameters )
        {
            var response=_productService.GetAllProduct(filters, parameters);
            return Ok(response);
        }

        [HttpGet("GetAllProductById/{id}")]
        public ActionResult GetAllProductById(int id)
        {
            var response = _productService.GetProductById(id);
            return Ok(response);
        }
    }
}
