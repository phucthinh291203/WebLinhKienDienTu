using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLBH.common.DAL;
using QLBH.common.Rsp;
using QLBH.DAL.Models;

namespace QLBH.DAL
{
	public class CartRep : GenericRep<WebDienTu15Context, Cart>
	{

		#region -- Overrides --
		public override Cart Read(int id)
		{
			var res = All.FirstOrDefault(p => p.Id == id);
			return res;
		}

		public int Remove(int id)
		{
			var m = base.All.First(i => i.Id == id);
			m = base.Delete(m);
			return m.Id;
		}

		#endregion

		public Cart GetCartByUserId(int userId)
		{
			var cart = All.Include(c => c.CartItems)
				 .ThenInclude(i => i.Product)
				 .FirstOrDefault(c => c.UserId == userId);

			return cart ?? throw new Exception("Cart not found for the given user ID."); /* cart == null tương đương cart ??*/
		}


		public CartItem AddItemToCart(int cartId, int productId, int quantity)
		{
			var cart = All.FirstOrDefault(p => p.Id == cartId);
			CartItem cartItem;
			if (cart != null)
			{
				cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
				if (cartItem != null)
					cartItem.Quantity += quantity;
				else
				{
					cartItem = new CartItem
					{
						CartId = cartId,
						ProductId = productId,
						Quantity = quantity
					};
					cart.CartItems.Add(cartItem);
				}

				Context.SaveChanges();
				return cartItem;
			}
			else
			{
				throw new Exception("Không tìm thấy giỏ hàng với ID đã cung cấp.");
			}
		}

		public CartItem UpdateItemQuantity(int cartId, int productId, int quantity)
		{
			var cart = All.FirstOrDefault(p => p.Id == cartId) ?? throw new Exception("Không tìm thấy giỏ hàng với ID đã cung cấp.");
			var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
			if (cartItem != null)
			{
				cartItem.Quantity = quantity;
				Context.SaveChanges();
			}

			return cartItem;
		}


		//public bool RemoveItemFromCart(int cartId, int productId)
		//{
		//	var cart = All.FirstOrDefault(p => p.Id == cartId);
		//	var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

		//	if (cartItem != null)
		//	{
		//		cart.CartItems.Remove(cartItem);
		//		Context.SaveChanges();
		//		return true;
		//	}

		//	return false;
		//}


		public SingleRsp RemoveProductFromCart(int cartItemId)
		{
			var res = new SingleRsp();
			using (var context = new WebDienTu15Context())
			{
				using var tran = context.Database.BeginTransaction();
				try
				{
					var cartItem = context.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
					if (cartItem == null)
					{
						res.SetError("Cart item not found.");
					}
					else
					{
						context.CartItems.Remove(cartItem);
						context.SaveChanges();
						tran.Commit();
						res.Data = cartItem;
					}
				}
				catch (Exception ex)
				{
					tran.Rollback();
					res.SetError(ex.StackTrace);
				}
			}
			return res;
		}

		public SingleRsp PlaceOrder(int cartId)
		{
			var res = new SingleRsp();
			using (var context = new WebDienTu15Context())
			{
				using var tran = context.Database.BeginTransaction();
				try
				{
					var cartItems = context.CartItems.Where(ci => ci.CartId == cartId).ToList();
					if (!cartItems.Any())
					{
						res.SetError("Cart is empty.");
						return res;
					}

					var h = new DonHang();


					h.IdKh = context.Carts.FirstOrDefault(c => c.Id == cartId).UserId;
					h.NgayDat = DateTime.Now;
					h.Slsp = cartItems.Sum(ci => ci.Quantity);
					h.TrangThai = "Đang xử lý";

					context.DonHangs.Add(h);
					context.SaveChanges();

					foreach (var item in cartItems)
					{
						var c = new Ctdh();
						c.Id = h.Id;
						c.IdSp = item.ProductId;
						c.Slsp = item.Quantity;
						c.DonGia = context.SanPhams.First(sp => sp.Id == item.ProductId).GiaBan;

						context.Ctdhs.Add(c);
					}

					context.SaveChanges();
					context.CartItems.RemoveRange(cartItems);
					context.SaveChanges();

					tran.Commit();
					res.Data = h;
				}
				catch (Exception ex)
				{
					tran.Rollback();
					res.SetError(ex.StackTrace);
				}
			}
			return res;
		}

		public SingleRsp GetAllOrders()
		{
			var res = new SingleRsp();
			using (var context = new WebDienTu15Context())
			{
				var orders = context.DonHangs
					.Select(o => new
					{
						o.Id,
						o.IdKh,
						o.NgayDat,
						o.Slsp,
						o.TrangThai,
						OrderDetails = o.Ctdhs.Select(od => new
						{
							od.IdSp,
							od.Slsp,
							od.DonGia,
							ProductName = od.IdSpNavigation.Ten,
							CategoryName = od.IdSpNavigation.IdLspNavigation.Ten
						}).ToList()
					}).ToList();

				res.Data = orders;
			}
			return res;
		}


		public SingleRsp GetSalesStatisticsByProductType()
		{
			var res = new SingleRsp();
			using (var context = new WebDienTu15Context())
			{
				var statistics = context.Ctdhs
					.Include(cthd => cthd.IdSpNavigation)
					.ThenInclude(sp => sp.IdLspNavigation)
					.GroupBy(cthd => new
					{
						cthd.IdSpNavigation.IdLspNavigation.Id,
						cthd.IdSpNavigation.IdLspNavigation.Ten
					})
					.Select(g => new
					{
						ProductTypeId = g.Key.Id,
						TenLoaiSanPham = g.Key.Ten,
						SoLuong = g.Sum(x => x.Slsp),
						TotalSales = g.Sum(x => x.Slsp * x.DonGia)
					})
					.ToList();

				res.Data = statistics;
			}
			return res;
		}
	}
}
