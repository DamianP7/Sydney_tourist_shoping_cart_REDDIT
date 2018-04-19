using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydney_tourist_shoping_cart_REDDIT
{
	static class Promotions
	{
		public static void OperaHouseFreeTicket(int inOH, int inBC, int inSK, ref int OH, ref int BC, ref int SK)
		{
			OH -= inOH / 3;
		}

		public static void SkyTowerFreeTicket(int inOH, int inBC, int inSK, ref int OH, ref int BC, ref int SK)
		{
			SK -= inOH;

			if (SK < 0)
				SK = 0;
		}

		public static int SydneyBridgeClimbDiscount(int inOH, int inBC, int inSK, ref int OH, ref int BC, ref int SK)
		{
			if (inBC > 4)
				return -20;
			else
				return 0;
		}
	}
}
