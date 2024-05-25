using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.common.Req;
using QLBH.common.Rsp;
using QLBH.DAL.Models;

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
        public IActionResult CreateProduct([FromBody] ProductReq _productReq)
        {
            var res = new SingleRsp();
            res = productSvc.CreateProduct(_productReq);
            return Ok(res);
        }

        [HttpPost("Search-Product")]
        public IActionResult SearchProduct([FromBody] SearchProductReq _searchProductReq)
        {
            var res = new SingleRsp();
            res = productSvc.SearchProduct(_searchProductReq);
            return Ok(res);
        }

        [HttpDelete("Delete-Product")]
        public IActionResult DeleteProduct(int id)
        {
            WebDienTu15Context context = new WebDienTu15Context();

            var pr = productSvc.Read(id);
            if (pr.Data != null)
            {
                context.Remove(pr.Data);
                context.SaveChanges();
                return Ok(pr);
            }
            else
                return NotFound();
        }


        [HttpPut("Update-Product")]
        public IActionResult UpdateProduct(int Id, ProductReq sanPhamReq)
        {
            var res = productSvc.UpdateProduct(Id, sanPhamReq);
            res.Data = sanPhamReq;
            return Ok(res);
        }


        [HttpGet("Get-all")]
        public IActionResult getAllProduct()
        {
            var res = new SingleRsp();

            // Assuming sanPhamSvc.All returns IEnumerable<SanPham>
            var products = productSvc.All.Select(p => new
            {
                MaSp = p.Id,
                TenSp = p.Ten,
                Gia = p.GiaBan,
                TenLoaiSp = p.IdLspNavigation.Ten,
                Image = p.HinhAnh // Assuming Image is a property representing the image
            }).ToList();

            res.Data = products;
            return Ok(res);

        }
    }
}
