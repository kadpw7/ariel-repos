using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery
{
    /// <summary>
    /// A class representing an employee.
    /// </summary>
    class Employee
    {
        string _name;
        string _id;       
        string _age;
        string _position;
        string _salary;

        public string GetVar(string varName) // Getting the private vars by a public function for further encapsulation
        {
            switch (varName)
            {
                case "name": return _name;
                case "id": return _id;             
                case "age": return _age;
                case "position": return _position;
                case "salary": return _salary.ToString();
               
                default: return "";
            }
        }

        public Employee(string name, string id, string position, string age, string salary)
        {
            _name = name;
            _id = id;           
            _age = age;
            _position = position;           
            _salary = salary;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}, Age: {2}, {3}, Salary: {4}NIS", _id, _name, _age, _position, _salary);
        }
    }

}
