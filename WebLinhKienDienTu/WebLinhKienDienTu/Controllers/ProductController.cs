using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.common.Req;
using QLBH.common.Rsp;

namespace WebLinhKienDienTu15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductSvc productSvc;

        public ProductController()
        {
            productSvc = new ProductSvc();
        }

        [HttpPost("Create-Product")]
        public IActionResult CreateProduct([FromBody] ProductReq productReq)
        {
            var res = new SingleRsp();
            res = productSvc.CreateProduct(productReq);
            return Ok(res);
        }
    }
}
