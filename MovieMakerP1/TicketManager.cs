using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMakerP1
{
    class TicketManager
    {

        //attributes
        private List<TicketHolder> ticketHolders = new List<TicketHolder>();

        private List<string> availableSnacks = new List<string>() { "Popcorn", "Chips", "Chocolate" };
        private List<float> snackPrices = new List<float>() { 2.5f, 1.5f, 2f };
        private List<string> availableDrinks = new List<string>() { "Fanta", "L&P" };
        private List<float> drinkPrices = new List<float>() { 2.5f, 1.5f };
        private float ticketPrice = 5f;
        private List<int> ageLimits = new List<int>() { 12 };
        private const int SEATLIMIT = 150;
        //constructor - contraucts a TicketManager object
        public TicketManager()
        {
        }

        public int GetSeatLimit()
        {
            return SEATLIMIT;
        }

        //Adds a ticket holder into the ticketHolders list
        public void AddTicketHolder(TicketHolder ticketHolder)
        {
            ticketHolders.Add(ticketHolder);
        }
        //returns true if purchaser's age meets the age requirement else it returns false
        public bool CheckAge(int buyerAge, int ageLimitIndex)
        {
            bool isOfAge = true;
            if (buyerAge < ageLimits[ageLimitIndex])
            {
                isOfAge = false;
            }
            return isOfAge;
        }
        public int CalculateAvailableSeats()
        {
            int sumTickets = 0;
            foreach (TicketHolder ticketHolder in ticketHolders)
            {
                sumTickets += ticketHolder.GetTickets();
            }
            return SEATLIMIT - sumTickets;
        }
        //returns true if there are enough available seats for purchase else returns false
        public bool CheckAvailableSeats(int requestedNoTickets)
        {
            if ((CalculateAvailableSeats() - requestedNoTickets) < 0)
            {
                return false;
            }
            return true;
        }
        //Adds new snack and snack prices to the snack and price lists
        public void AddSnack(string snack, float price)
        {
            availableSnacks.Add(snack);
            snackPrices.Add(price);
        }
        //Adds new age  to the ageLimit lists
        public void AddAgeLimit(int newAge)
        {
            ageLimits.Add(newAge);
            ageLimits.Sort();
        }
        //sets a new value for the ticket price
        public void ChangeTicketPrice(float newTicketPrice)
        {
            ticketPrice = newTicketPrice;
        }
        //calculate gross profit for ticket sales
        private float CalculateTicketGrossProfit()
        {
            int sumTicketsSold = 0;
            foreach (TicketHolder ticketHolder in ticketHolders)
            {
                sumTicketsSold += ticketHolder.GetTickets();
            }

            return sumTicketsSold * ticketPrice;
        }
        private List<int> SumItemsSold(string itemType)
        {
            List<string> availableItems;
            //gets correct available item list 
            if (itemType.Equals("snacks"))
            {
                availableItems = availableSnacks;
            }
            else
            {
                availableItems = availableDrinks;
            }
            //collection storing the total quantities sold for items sold
            List<int> sumItemsSold = new List<int>();
            for (int availableSItemIndex = 0; availableSItemIndex < availableItems.Count; availableSItemIndex++)
            {
                sumItemsSold.Add(0);
            }
            foreach (TicketHolder ticketHolder in ticketHolders)
            {
                List<int> orderedItems, itemsQuantities;
                // stores the correct item order and its quantities 
                if (itemType.Equals("snacks"))
                {
                    orderedItems = ticketHolder.GetSnackOrder();
                    itemsQuantities = ticketHolder.GetSnackQuantity();
                }
                else
                {
                    orderedItems = ticketHolder.GetDrinkOrder();
                    itemsQuantities = ticketHolder.GetDrinkQuantity();
                }

                //loop through the ordered item
                for (int orderIndex = 0; orderIndex < orderedItems.Count; orderIndex++)
                {
                    //loop through available item
                    for (int itemIndex = 0; itemIndex < availableItems.Count; itemIndex++)
                    {
                        //check if ticketHolder has purchased item
                        if (itemIndex == orderedItems[orderIndex])
                        {
                            //add quantity to sumItemsSold
                            sumItemsSold[itemIndex] += itemsQuantities[orderIndex];
                        }
                    }
                }
            }
            return sumItemsSold;
        }
        private float CalculateItemsGrossProfit()
        {
            //calculate total gross profit by multiply the sum quantity of each item with its price
            float grossProfit = 0;
            for (int snackIndex = 0; snackIndex < availableSnacks.Count; snackIndex++)
            {
                grossProfit += SumItemsSold("snacks")[snackIndex] * snackPrices[snackIndex];
            }
            for (int drinkIndex = 0; drinkIndex < availableDrinks.Count; drinkIndex++)
            {
                grossProfit += SumItemsSold("drinks")[drinkIndex] * drinkPrices[drinkIndex];
            }
            //calculate gross profit for snack and drick sales
            return grossProfit;
        }
        //calculate the cost for total snack and drink sales
        private float CalculateSnackDrinkTotalCost()
        {
            //cost = 100 x (total of sales / 120)
            return 100 * (CalculateItemsGrossProfit() / 120);
        }
        //returns calculated total profit
        public float CalculateTotalProfit()
        {
            // total profit = ticket profit + (snacks and drinks gross profit - cost of snacks and drinks)
            return (float)Math.Round(CalculateTicketGrossProfit() + (CalculateItemsGrossProfit() - CalculateSnackDrinkTotalCost()),2);
        }
        //returns a string listing the total number for snacks ordered
        public string TotalSnacksOrdered()
        {
            string summary = "----- Total Ordered Snacks -----\n";
            for (int snackIndex = 0; snackIndex < availableSnacks.Count; snackIndex++)
            {
                summary += availableSnacks[snackIndex] + "\tX\t" + SumItemsSold("snacks")[snackIndex] + "\n";
            }
            return summary;
        }
        public string DisplayTotalProfit()
        {
            return $"Total Profit: ${CalculateTotalProfit()}";
        }
        public override string ToString()
        {
            string toString = "";

            foreach(int age in ageLimits)
            {
                toString += age+ "\n";
            }
            return toString;
        }
        
        
        
        
        
        
        
        
        
        
        public void AddSnacksDrinksOrder(List<int> sOrder, List<int> sQuantity, List<int> dOrder, List<int> dQuantity)
        {


        }





























    }
}
