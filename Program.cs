using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydney_tourist_shoping_cart_REDDIT
{
	class Program
	{
		static void Main(string[] args)
		{
			Cart myCart = new Cart();
			myCart.AddFreeTicketPromo(Promotions.OperaHouseFreeTicket);
			myCart.AddFreeTicketPromo(Promotions.SkyTowerFreeTicket);
			myCart.AddDicountBC(Promotions.SydneyBridgeClimbDiscount);

			myCart.NewOrder();

			Console.ReadKey();
		}
	}
}
