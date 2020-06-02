using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bakery.Order_Handlers
{
    class OrderInfo
    {
        public string OrderType { get; set; }
        public string PayingMethod { get; set; }
        public List<Dish> OrderedDishes { get; set; }

        public OrderInfo(List<Dish> orderedDishes)
        {
            OrderedDishes = new List<Dish>();

            foreach (Dish dish in orderedDishes)
                OrderedDishes.Add(dish);
        }

        public override string ToString()
        {
            string output;

            output = "Order Type: " + OrderType + Environment.NewLine;
            output += "Payed by: " + PayingMethod + Environment.NewLine + Environment.NewLine;
            output += "Dishes ordered:" + Environment.NewLine + Environment.NewLine;

            foreach(Dish dish in OrderedDishes)
                output += dish.ToString() + Environment.NewLine;

            output += "-------------------------------------------------------------" + Environment.NewLine;

            return output;
        }

    }
}
