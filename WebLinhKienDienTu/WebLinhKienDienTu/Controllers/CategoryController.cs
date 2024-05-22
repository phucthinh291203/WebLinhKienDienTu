using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.common.Req;
using QLBH.common.Rsp;
using QLBH.DAL.Models;

namespace WebLinhKienDienTu15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategorySvc categorySvc;
        public CategoryController()
        {
            categorySvc = new CategorySvc();
        }
        [HttpPost("Get-By-Id")]
        public IActionResult GetCategoryByID([FromBody] SimpleReq simpleReq)
        {
            var res = new SingleRsp();
            res = categorySvc.Read(simpleReq.Id);
            return Ok(res);
        }

        [HttpPost("Get-All")]
        public IActionResult GetAllCategory()
        {
            var res = new SingleRsp();
            res.Data = categorySvc.All;
            return Ok(res);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CategoryReq loaiSpReq)
        {
            var res = new SingleRsp();
            res = categorySvc.CreateCategory(loaiSpReq);
            res.Data = loaiSpReq;
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpDate(int Id, CategoryReq loaiSpReq)
        {
            var res = categorySvc.UpdateCategory(Id, loaiSpReq);
            res.Data = loaiSpReq;
            return Ok(res);
        }

        [HttpDelete("Deleta-By-Id")]
        public IActionResult DeleteById(int id)
        {
            WebDienTuContext context = new WebDienTuContext();
            var l = categorySvc.Read(id);
            if (l.Data != null)
            {
                context.Remove(l.Data);
                context.SaveChanges();
                return Ok(l);
            }
            return NotFound();
        }

        [HttpPost("Seach-By-Name")]
        public IActionResult SeachByName(SearchCateByNameReq search)
        {
            var res = new SingleRsp();
            res = categorySvc.SearchCategory(search);
            return Ok(res);
        }


    }
}
