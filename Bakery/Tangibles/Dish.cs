using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bakery
{
    /// <summary>
    /// A class representing a dish
    /// </summary>
    public class Dish
    {
        string _name;
        string _id;
        string _type;
        string _info;
        double _price;

        public double GetPrice()
        {
            return _price;
        }

        public string GetVar(string varName) // Getting the private vars by a public function for further encapsulation
        {
            switch(varName)
            {
                case "name": return _name;
                case "id": return _id;
                case "type": return _type;
                case "info": return _info;
                default: return "";
            }
        }

        public Dish(string name, string id, string type, string info, double price)
        {
            _name = name;
            _id = id;
            _type = type;
            _info = info;
            _price = price;
        }

        public override string ToString()
        {
            if (_info == "")
                return string.Format("{0}. {1}, {2} {3}NIS", _id, _name, _type, _price);
            else
                return string.Format("{0}. {1}, {2} ({3}) {4}NIS", _id, _name, _type, _info, _price);
        }
    }
}
