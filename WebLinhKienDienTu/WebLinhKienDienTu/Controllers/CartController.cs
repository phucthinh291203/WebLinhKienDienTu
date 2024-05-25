using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;

namespace WebLinhKienDienTu15.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private CartSvc cartSvc;
		public CartController()
		{
			cartSvc = new CartSvc();
		}
		[HttpPost("add-item")]
		public IActionResult AddItemToCart(int userId, int productId, int quantity)
		{
			var res = cartSvc.AddItemToCart(userId, productId, quantity);
			return Ok(res);
		}

		[HttpPost("update-item-quantity")]
		public IActionResult UpdateItemQuantity(int userId, int productId, int quantity)
		{
			var res = cartSvc.UpdateItemQuantity(userId, productId, quantity);
			return Ok(res);
		}

		[HttpPost("place-order")]
		public IActionResult PlaceOrder(int cartId)
		{
			var res = cartSvc.PlaceOrder(cartId);
			return Ok(res);
		}

		//[HttpDelete("remove-item")]
		//public IActionResult RemoveItemFromCart(int userId, int productId)
		//{
		//	var res = cartSvc.RemoveItemFromCart(userId, productId);
		//	return Ok(res);
		//}

		[HttpDelete("remove-product")]
		public IActionResult RemoveProductFromCart(int cartItemId)
		{
			var res = cartSvc.RemoveProductFromCart(cartItemId);
			return Ok(res);
		}


		[HttpGet("get-cart-by-id")]
		public IActionResult GetCartByUserId(int userId)
		{
			var res = cartSvc.GetCartByUserId(userId);
			return Ok(res);
		}


		[HttpGet("sales-statistics-by-product-type")]
		public IActionResult GetSalesStatisticsByProductType()
		{
			var res = cartSvc.GetSalesStatisticsByProductType();
			return Ok(res);
		}
	}
}
