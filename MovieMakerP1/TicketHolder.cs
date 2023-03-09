using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMakerP1
{
    class TicketHolder
    {
        //attributes or fields
        private string name;
        private int age;
        private int numberTickets;
        private bool credit;
        //stores the indexes of the snacks that as been ordered
        private List<int> snackOrder = new List<int>();
        private List<int> snackQuantity = new List<int>();
        //stores the indexes of the drinks that as been ordered
        private List<int> drinkOrder = new List<int>();
        private List<int> drinkQuantity = new List<int>();
        //constructor - cronstructs an object of this class
        public TicketHolder(string name, int age, int nTickets)
        {
            this.name = name;
            this.age = age;
            numberTickets = nTickets;
        }
        //returns the value in the private age variable
        public int GetAge()
        {
            return this.age;
        }
        //Sets the value of the private age variable
        public void SetAge(int newAge)
        {
            this.age = newAge;
        }
        public int GetTickets()
        {
            return numberTickets;
        }
        //get the snack order
        public List<int> GetSnackOrder()
        {
            return snackOrder;
        }
        //get the snack order quantities
        public List<int> GetSnackQuantity()
        {
            return snackQuantity;
        }
        //get the drinks order
        public List<int> GetDrinkOrder()
        {
            return drinkOrder;
        }
        //get the drinks order quantities
        public List<int> GetDrinkQuantity()
        {
            return drinkQuantity;
        }
        //Sets the value of the private credit variable
        public void SetCredit(bool newPaymentType)
        {
            credit = newPaymentType;
        }
        //add snaks and quantities to the snacksOrder list and snacksQuantities Lists
        public void AddSnacks(List<int> snacks, List<int> quantities)
        {
            snackOrder = snacks;
            snackQuantity = quantities;
        }

        //add drinks and quantities to the drinksOrder list and drinksQuantities Lists
        public void AddDrinks(List<int> drinks, List<int> quantities)
        {
            drinkOrder = drinks;
            drinkQuantity = quantities;
        }
        // returns string stating if the ticket holder is paying cash or card
        private string PaymentType()
        {
            string paymentType = "card";
            if (credit == false)
            {
                paymentType = "cash";
            }
            return paymentType;
        }
        //returns the total cost of the purchased items
        public float CalculateTotalCost(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            float totalCost = 0f;
            //loop through snack order and calculate the cost for each snack
            for (int snackIndex = 0; snackIndex < snackOrder.Count; snackIndex++)
            {
                float snackPrice = sPrices[snackOrder[snackIndex]];
                //add the cost of each snack to totalCost
                totalCost += snackPrice * snackQuantity[snackIndex];
            }

            //loop through drink order and calculate the cost for each drink
            for (int drinkIndex = 0; drinkIndex < drinkOrder.Count; drinkIndex++)
            {
                float drinkPrice = dPrices[drinkOrder[drinkIndex]];
                //add the cost of each drink to totalCost
                totalCost += drinkPrice * drinkQuantity[drinkIndex];
            }
            totalCost += CalculateTicketCost(ticketPrice);

            return totalCost;
        }
        //return ticket summary
        private string TicketSummary(float ticketPrice)
        {
            return "-------------------------\n" + $"{numberTickets} x Tickets\t${CalculateTicketCost(ticketPrice)}\n-------------------------\n";
        }
        //calculate ticket cost
        public float CalculateTicketCost(float ticketPrice)
        {
            return numberTickets * ticketPrice;
        }
        //return a summary of the drinks and snack order
        private string SnackDrinkSummary(List<string> sList, List<float> sPrices, List<string> dList, List<float> dPrices)
        {
            string summary = "Snacks and Drinks\n";
            //loop through snack orders and add quantity, snack, total item cost to summary
            for (int snackIndex = 0; snackIndex < snackOrder.Count; snackIndex++)
            {
                summary += snackQuantity[snackIndex] + " x   " + sList[snackOrder[snackIndex]] + "\t$" + (snackQuantity[snackIndex] * sPrices[snackOrder[snackIndex]]) + "\n";
            }
            for (int drinkIndex = 0; drinkIndex < drinkOrder.Count; drinkIndex++)
            {
                summary += drinkQuantity[drinkIndex] + " x   " + dList[drinkOrder[drinkIndex]] + "\t$" + (drinkQuantity[drinkIndex] * dPrices[drinkOrder[drinkIndex]]) + "\n";
            }
            return summary;
        }
        //check if a surcharge is required
        //private bool GetSurcharge()
        //{
        //    return credit;
        //}
        // return string displaying surcharge cost
        private string SurchargeSummary(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            string summary = "";
            if (credit)
            {
                summary += "Surcharge\t$" + CalculateSurcharge(sPrices, dPrices, ticketPrice);
            }
            return summary;
        }
        // return the surcharge amount
        private float CalculateSurcharge(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            float surcharge = CalculateTotalCost(sPrices, dPrices, ticketPrice) * 0.05f;
            return surcharge;
        }
        //Calculate the total amount to be paid
        private float CalculateTotalPayment(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            float totalPayment = CalculateTotalCost(sPrices, dPrices, ticketPrice);
            if (credit)
            {
                totalPayment += CalculateSurcharge(sPrices, dPrices, ticketPrice);
            }

            return totalPayment;
        }

        //Return a summary of the total amount to be paid
        private string TotalPaymentSummary(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            return "Total\t$" + CalculateTotalPayment(sPrices, dPrices, ticketPrice);
        }

        //returns a string diaplaying the reciept for the puchased items
        public string GenerateReciept(float tPrice, List<string> sList, List<float> sPrices, List<string> dList, List<float> dPrices)
        {
            string reciept = $"Name: {name}\nAge: {age}\nPayment Type: {PaymentType()}\n" +
                $"{TicketSummary(tPrice)}\n{SnackDrinkSummary(sList, sPrices, dList, dPrices)}\n" +
                $"{SurchargeSummary(sPrices, dPrices, tPrice)}\n{TotalPaymentSummary(sPrices, dPrices, tPrice)}";

            return reciept;
        }
        // returns a string collating all the values stored in the private variables
        public override string ToString()
        {
            return $"Name: {name}\tAge: {age}\tNoTickets: {numberTickets}";
        }

    }
}
