using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bakery
{
    public class Menu
    {
        public List<Dish> Dishes;

        public Menu(List<Dish> dishes) { Dishes = dishes; } // Dependency injection DP, as every menu must have a list of dishes

        public double GetPriceByIndex(int index)
        {
            return Dishes[index].GetPrice();
        }

        public void Show() 
        {

        }
        
    }
}
