using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bakery
{
    static class EmployeeHandler
    {
        public static List<Employee> Employees;

        public static void AddEmployee(string name, string id, string age, string position, string salary, int index, out bool result)
        {

            result = false;

            if (name == "" || id == "" || age == "" || position == "" || salary == "")
                MessageBox.Show("Please fill in all of the fields");

            else if (!double.TryParse(salary, out _)) // Using a discard value as we don't really need an out argument.
                MessageBox.Show("Invalid salary");

            else if (!int.TryParse(age, out _))
                MessageBox.Show("Invalid age");

            else
            {
                Employee employee = new Employee(name, id, age, position, salary);

                if (index == -1)
                    Employees.Add(employee);
                else
                    Employees[index] = employee;

                result = true;
            }
        }
    }
}
