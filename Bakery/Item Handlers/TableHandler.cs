using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Bakery
{
    class TableHandler // Using the singleton design pattern as we only need one TableHandler instance.
    {

        Table[] Tables;

        static TableHandler tableHandlerInstance = null;

        TableHandler()
        {
            Tables = new Table[6];

            string[] _names = new string[] { "Table1", "Table2", "Table3", "Table4", "Table5", "Table6" };
            string[] _locations = new string[] { "1A", "1A", "2B", "2B", "1F", "3F" };

            for (int i = 0; i < 6; i++)
            {
                Tables[i] = new Table();
                Tables[i].Chairs = 4;
                Tables[i].IsOccupied = false;
                Tables[i].TableName = _names[i];
                Tables[i].Location = _locations[i];
            }
        }

        public static TableHandler Instance
        {
            get
            {
                if (tableHandlerInstance == null)
                    tableHandlerInstance = new TableHandler();

                return tableHandlerInstance;
            }
        }

        public void PrintTableStates()
        {

            List<string> details = new List<string>();

            foreach (Table t in Tables)
            {
                details.AddRange(new List<string>() { t.TableName, "chairs: " + t.Chairs, "Location: " + t.Location });
                if (t.IsOccupied) details.Add("Occupied: Yes"); else details.Add("Occupied: No");
                details.Add("----------------------");
            }

            View.DisplayListBox(details.ToArray());
        }

        public void ChangeTableOccupationState(int num, bool state)
        {
            Tables[num - 1].IsOccupied = state;
            if (state)
                MessageBox.Show("Table " + num + " is now set as occupied");
            else
                MessageBox.Show("Table " + num + " is now set as unoccupied");
        }

        public void ChangeChairsInTable(int num)
        {
            string chairs = "";
            chairs = View.InputTxt("Chairs", "New no. of chairs in Table" + num + ":", chairs);

            for (int i = 1; i <= 12; i++)
                if (chairs == i.ToString())
                {
                    Tables[num - 1].Chairs = Convert.ToInt32(chairs);
                    MessageBox.Show("Chairs in table " + num + " updated to " + chairs);
                    return;
                }

            MessageBox.Show("Invalid input. must be a number 1-12");
        }

        public int GetTableNumFromUser()
        {
            string input = "";
            input = View.InputTxt("Chairs", "Table number:", input);
            if (input == "") input = "cancel"; // This means we clicked the cancel button

            for (int i = 1; i <= 6; i++)
                if (input == i.ToString())
                    return Convert.ToInt32(input);

            if (input != "cancel")
                MessageBox.Show("Invalid input. must be a number 1-6");
            return 0;
        }

    }
}
