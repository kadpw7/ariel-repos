using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bakery
{
    public sealed class DishHandler // implementing the Singleton design pattern, as we need just 1 DishHandler instance.
    {

        public List<Dish> Dishes { get; set; } // All of the dishes

        public Menu MenuProp { get; set; }

        static DishHandler dishHandlerInstance = null;

        DishHandler()
        {
            Dishes = new List<Dish>();
            MenuProp = new Menu(Dishes);
            MenuExists = false;
        }

        public static DishHandler Instance
        {
            get
            {
                if (dishHandlerInstance == null) // Only initializing the instance if it is null                
                    dishHandlerInstance = new DishHandler();

                return dishHandlerInstance;
            }
        }

        public bool MenuExists { get; set; }

        /// <summary>
        /// Adding a dish to the dish list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="info"></param>
        /// <param name="strPrice"></param>
        /// 
        public void AddDish(string name, string id, string type, string info, string strPrice, int index, out bool result)
        {
            result = false;
            if (name == "" || id == "")
                MessageBox.Show("A dish name and ID must be entered");

            else if (!double.TryParse(strPrice, out double price))
                MessageBox.Show("Invalid price");

            else
            {
                Dish dish = new Dish(name, id, type, info, price);

                if (index == -1)
                    Dishes.Add(dish);
                else
                    Dishes[index] = dish;

                result = true;
            }
        }

        public List<Dish> GetDishesFromACheckedList(CheckedListBox fromList)
        {
            List<Dish> chosenDishes = new List<Dish>();

            // Transfering the checked items to the chosenDishes list.
            for (int i = 0; i < fromList.Items.Count; i++)
                if (fromList.GetItemChecked(i))
                    chosenDishes.Add(Dishes[i]);

            return chosenDishes;
        }

        public void CreateMenu(CheckedListBox fromList)
        {
            List<Dish> menuDishes = GetDishesFromACheckedList(fromList);

            if (fromList.Items.Count == 0)
            {
                MessageBox.Show("Please pick at least one dish");
                return;
            }

            MenuProp = new Menu(menuDishes);
            MessageBox.Show("New Menu Created");
            MenuExists = true;
        }
    }
}

