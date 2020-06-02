using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Forms;

namespace Bakery
{
    class SupplyHandler // Using the singleton design pattern as we only need one SupplyHandler instance.
    {
        public List<Supply> Supplies;

        public SupplyHandler()
        {
            Supplies = new List<Supply>();
        }

        static SupplyHandler supplyHandlerInstance = null;

        public static SupplyHandler Instance
        {
            get
            {
                if (supplyHandlerInstance == null)
                    supplyHandlerInstance = new SupplyHandler();

                return supplyHandlerInstance;
            }
        }

        public void AddSupply(string name, string weight, string storageTemp, string priceForKilo, int index, out bool result)
        {

            result = false;
            if (name == "" || weight == "" || storageTemp == "" || priceForKilo == "")
                MessageBox.Show("Please fill in all of the fields");
            
            else if (!double.TryParse(weight, out _))          
                MessageBox.Show("Invalid weight");
            
            else if (!double.TryParse(storageTemp, out _))          
                MessageBox.Show("Invalid temp");

            else if (!double.TryParse(priceForKilo, out _))
                MessageBox.Show("Invalid price");

            else
            {
                Supply supply = new Supply(name, weight, storageTemp, priceForKilo);

                if (index == -1)
                    Supplies.Add(supply);
                else
                    Supplies[index] = supply;

                result = true;
            }
        }

    }
}
