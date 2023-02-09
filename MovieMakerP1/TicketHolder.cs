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
        private List<string> snackOrder = new List<string>();
        private List<int> snackQuantity = new List<int>();
        private List<string> drinkOrder = new List<string>();
        private List<int> drinkQuantity = new List<int>();

        //constructor - constructs an object of this class
        public TicketHolder()
        { 

        
        
        
        
        }
        // returns the value in the private age variable
        public int GetAge() 
        {
            return 0;
        
        }
        //sets the value of the private age variable
        public void SetAge(int newAge) 
        {

        }
        //  returns the total cost of the total cost
        public float CalculateTotalCost() 
        {
            return 0.0f;
        }
        // returns a string displaying a reciept
        public string GenerateReciept() 
        {
            return "";
        }
        // returns a string collating all the values stored in the private variables
        public override string ToString() 
        {
            return "";
        }






    }
}
