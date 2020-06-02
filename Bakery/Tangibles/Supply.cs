using System;
using System.Collections.Generic;

namespace Bakery
{
    public class Supply
    {
        string _name;
        string _weight;
        string _storageTemp;
        string _priceForKilo;

        public double Price { get { return Convert.ToDouble(_priceForKilo) * Convert.ToDouble(_weight); } }

        public string GetVar(string varName) // Getting the private vars by a public function for further encapsulation
        {
            switch (varName)
            {
                case "name": return _name;
                case "weight": return _weight;
                case "temp": return _storageTemp;
                case "price": return _priceForKilo;
                default: return "";
            }
        }

        public Supply(string name, string weight, string storageTemp, string priceForKilo)
        {
            _name = name;
            _weight = weight;
            _storageTemp = storageTemp;
            _priceForKilo = priceForKilo;
        }

        public override string ToString()
        {
             return string.Format("{0}, {1}, Storage temp (celsius): {2}, Price for kilo: {3}NIS", _name, _weight, _storageTemp, _priceForKilo);
        }

    }
}
