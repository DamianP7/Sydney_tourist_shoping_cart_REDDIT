using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sydney_tourist_shoping_cart_REDDIT
{
	enum ListOfIds
	{
		OH,
		BC,
		SK
	};
	delegate void FreeTicket(int inOH, int inBC, int inSK, ref int OH, ref int BC, ref int SK);
	delegate int Discount(int inOH, int inBC, int inSK, ref int OH, ref int BC, ref int SK);

	class Cart
	{
		private const int priceOH = 300;
		private const int priceBC = 110;
		private const int priceSK = 30;

		private List<List<ListOfIds>> completeOrder;
		private List<FreeTicket> freeTickets;
		private List<List<Discount>> discounts;

		public Cart()
		{
			completeOrder = new List<List<ListOfIds>>();
			freeTickets = new List<FreeTicket>();
			discounts = new List<List<Discount>>();
			discounts.Add(new List<Discount>());
			discounts.Add(new List<Discount>());
			discounts.Add(new List<Discount>());
		}

		private ListOfIds ReturnId(string ticket)
		{
			switch (ticket)
			{
				case "OH":
					return ListOfIds.OH;
					break;
				case "BC":
					return ListOfIds.BC;
					break;
				case "SK":
					return ListOfIds.SK;
					break;
			}
			return ListOfIds.SK;
		}

		private List<ListOfIds> GetTickets(string line)
		{
			List<ListOfIds> list = new List<ListOfIds>();
			string temp;
			temp = "";

			foreach (char c in line)
			{
				if (c == ' ')
				{
					temp = "";
				}
				else
				{
					temp += c;
				}

				if (temp.Length == 2)
				{
					list.Add(ReturnId(temp));
				}
			}
			return list;
		}

		private void GetOrder()
		{
			string line;
			for (; ; )
			{
				line = Console.ReadLine();

				if (line.Length < 2)
					break;

				completeOrder.Add(GetTickets(line));
			}
		}

		private int Counter(List<ListOfIds> list, ListOfIds ticketToCount)
		{
			int counter = 0;
			foreach (ListOfIds ticket in list)
			{
				if (ticket == ticketToCount)
				{
					counter++;
				}
			}
			return counter;
		}

		private int Check(List<ListOfIds> list)
		{
			int promoPriceOH = priceOH;
			int promoPriceBC = priceBC;
			int promoPriceSK = priceSK;
			int amountOfOH;
			int amountOfBC;
			int amountOfSK;

			int promoAmountOfOH = amountOfOH = Counter(list, ListOfIds.OH);
			int promoAmountOfBC = amountOfBC = Counter(list, ListOfIds.BC);
			int promoAmountOfSK = amountOfSK = Counter(list, ListOfIds.SK);

			foreach (FreeTicket promo in freeTickets)
			{
				promo(amountOfOH, amountOfBC, amountOfSK, ref promoAmountOfOH, ref promoAmountOfBC, ref promoAmountOfSK);
			}

			foreach (Discount dis in discounts[0])
			{
				promoPriceOH += dis(amountOfOH, amountOfBC, amountOfSK, ref promoAmountOfOH, ref promoAmountOfBC, ref promoAmountOfSK);
			}
			foreach (Discount dis in discounts[1])
			{
				promoPriceBC += dis(amountOfOH, amountOfBC, amountOfSK, ref promoAmountOfOH, ref promoAmountOfBC, ref promoAmountOfSK);
			}
			foreach (Discount dis in discounts[2])
			{
				promoPriceSK += dis(amountOfOH, amountOfBC, amountOfSK, ref promoAmountOfOH, ref promoAmountOfBC, ref promoAmountOfSK);
			}


			int price =  (promoAmountOfOH * promoPriceOH) + (promoAmountOfBC * promoPriceBC) + (promoAmountOfSK * promoPriceSK);

			return price;
		}

		private void ShowOrder(List<ListOfIds> list)
		{
			foreach (ListOfIds ticket in list)
			{
				switch (ticket)
				{
					case ListOfIds.OH:
						Console.Write("OH");
						break;
					case ListOfIds.BC:
						Console.Write("BC");
						break;
					case ListOfIds.SK:
						Console.Write("SK");
						break;
				}
				Console.Write(", ");
			}
		}

		private void Checkout()
		{
			foreach (List<ListOfIds> list in completeOrder)
			{
				ShowOrder(list);
				Console.WriteLine(" = {0}", Check(list));
			}
		}

		public void AddFreeTicketPromo(FreeTicket promo)
		{
			freeTickets.Add(promo);
		}

		public void AddDicountOH(Discount discount)
		{
			discounts[0].Add(discount);
		}
		public void AddDicountBC(Discount discount)
		{
			discounts[1].Add(discount);
		}
		public void AddDicountSK(Discount discount)
		{
			discounts[2].Add(discount);
		}

		public void NewOrder()
		{
			GetOrder();
			Checkout();
		}
	}

}