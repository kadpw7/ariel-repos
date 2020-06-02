using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bakery.Order_Types;
using Bakery.PaymentTypes;
using System.Drawing;
using Bakery.Order_Handlers;
using System.Data;

/* Design patterns used:
 * Bridge(Order Handlers), Adapter(Membership), DI(Menu), Singleton(Handlers, except the Employee which is static)*/

namespace Bakery
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Width = 1000; Height = 800;

            View.PanelInit();
            View.ListBoxInit(listBox); View.ListBoxInit(chkList);

            //The combo box will be set to the first option when it is shown
            cbType.SelectedIndex = 0;

            EmployeeHandler.Employees = new List<Employee>();
            OrdersDatabase.OrdersInfo = new List<OrderInfo>();

            View.InfoMenuInit(ref txtBoxinfo, ref btnInfoX);
            View.OrdersInfoMenuInit(ref txtBoxOrdersInfo, ref btnOrdersInfoX);        
        }

        #region Viewing actions

        private void ResetSelections(ref Panel p)
        {
            p.Hide();
            p.Tag = "";

            foreach (var ctrl in p.Controls)
            {
                if (ctrl is TextBox) ((TextBox)ctrl).Text = "";
                else if (ctrl is CheckBox) ((CheckBox)ctrl).Checked = false;
                else if (ctrl is ComboBox) ((ComboBox)ctrl).SelectedIndex = 0;
            }
        }

        private void PriceLblsShow(bool show)
        {
            if (show)
            {
                lblOrderPrice.Show();
                lblTotalPrice.Show();
            }

            else
            {
                lblOrderPrice.Hide();
                lblTotalPrice.Hide();
            }
        }

        private void HidePanels()
        {
            foreach (Panel p in Controls.OfType<Panel>())
                p.Hide();
        }

        // Using a generic function
        private void ViewItems<T>(List<T> items)
        {
            int count = items.Count;
            if (count == 0) { MessageBox.Show("No items yet"); return; }

            listBox.Items.Clear();

            foreach (var item in items)
                listBox.Items.Add(item.ToString());

            panelView.Show();
        }

        private void viewAllDishesStripItem_Click(object sender, EventArgs e)
        {
            ViewItems(DishHandler.Instance.Dishes);
            btnOkView.Tag = "";
        }

        private void viewAllSuppliesStripItem_Click(object sender, EventArgs e)
        {
            ViewItems(SupplyHandler.Instance.Supplies);
            btnOkView.Tag = "";
        }

        private void viewEmployeesStripItem1_Click(object sender, EventArgs e)
        {
            ViewItems(EmployeeHandler.Employees);
            btnOkView.Tag = "";
        }

        private void viewAllOrdersStripItem_Click(object sender, EventArgs e)
        {
            txtBoxOrdersInfo.Text = "ORDERS:" + Environment.NewLine + Environment.NewLine;

            foreach (OrderInfo orderInfo in OrdersDatabase.OrdersInfo)
            {
                txtBoxOrdersInfo.Text += orderInfo.ToString();
            }

            txtBoxOrdersInfo.Show();
            btnOrdersInfoX.Show();
        }

        #endregion

        #region Orders
        private void OrderMenuStripClicked(string kind)
        {
            if (!DishHandler.Instance.MenuExists)
            {
                MessageBox.Show("Please create a menu first"); return;
            }

            View.PopulateChklstFromList(DishHandler.Instance.MenuProp.Dishes, ref chkList);

            lblOrderPrice.Text = "0";

            panelPayment.Tag = kind;

            panelChooseItems.Show();

            btnOkChklst.Tag = "OkOrder";

            PriceLblsShow(true);
        }

        #region OrderTypesStripItems
        // The order types click events from the menu
        private void counterStripItem_Click(object sender, EventArgs e)
        {
            OrderMenuStripClicked("counter");
        }

        private void phoneStripItem_Click(object sender, EventArgs e)
        {
            OrderMenuStripClicked("phone");
        }

        private void onlineStripItem_Click(object sender, EventArgs e)
        {
            OrderMenuStripClicked("online");
        }
        #endregion

        private void btnOkPaying_Click(object sender, EventArgs e)
        {
            OrderMaker orderMaker = new OrderMaker();
            string orderType = panelPayment.Tag.ToString();

            Order order = orderMaker.MakeOrder(orderType);

            order.paymentType = radioCash.Checked ? orderMaker.CashOrCredit("cash") : orderMaker.CashOrCredit("credit");

            panelPayment.Hide();
            order.Show();

            int count = OrdersDatabase.OrdersInfo.Count;
            OrdersDatabase.OrdersInfo[count - 1].OrderType = orderType;
            OrdersDatabase.OrdersInfo[count - 1].PayingMethod = radioCash.Checked ? "Cash" : "Credit";
        }

        #endregion

        #region btnOkViewClick
        /// <summary>
        /// Ok click for viewing items or for an item editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOkView_Click(object sender, EventArgs e)
        {
            panelView.Hide();

            if (btnOkView.Tag.ToString() == "OkEditDish")
            {
                // The index selected by the user is the one to be edited.
                panelAddEditDish.Tag = listBox.SelectedIndex.ToString();

                // Now we populate the fields in the "Add Dish" to the values of the dish that we chose 
                int index = Convert.ToInt32(panelAddEditDish.Tag);

                Dish dish = DishHandler.Instance.Dishes[index];
                txtBoxDishName.Text = dish.GetVar("name");
                txtBoxDishID.Text = dish.GetVar("id");
                txtBoxDishPrice.Text = dish.GetPrice().ToString();

                panelAddEditDish.Show();
            }

            else if (btnOkView.Tag.ToString() == "OkEditSupply")
            {
                panelAddEditSupply.Tag = listBox.SelectedIndex.ToString();

                int index = Convert.ToInt32(panelAddEditSupply.Tag);

                Supply supply = SupplyHandler.Instance.Supplies[index];
                txtBoxSupplyName.Text = supply.GetVar("name");
                txtBoxSupplyWeight.Text = supply.GetVar("weight");
                txtBoxSupplyTemp.Text = supply.GetVar("temp");
                txtBoxSupplyPriceForKilo.Text = supply.GetVar("price");

                panelAddEditSupply.Show();
            }

            else  if (btnOkView.Tag.ToString() == "OkEditEmployee")
            {
                panelAddEditEmployee.Tag = listBox.SelectedIndex.ToString();

                int index = Convert.ToInt32(panelAddEditEmployee.Tag);

                Employee employee = EmployeeHandler.Employees[index];
                txtBoxEmployeeName.Text = employee.GetVar("name");
                txtBoxEmployeeId.Text = employee.GetVar("id");
                txtBoxEmployeeAge.Text = employee.GetVar("age");
                txtBoxEmployeePosition.Text = employee.GetVar("position");
                txtBoxEmployeeSalary.Text = employee.GetVar("salary");


                panelAddEditEmployee.Show();               
            }
        }
        #endregion

        #region CheckList

        /// <summary>
        /// This event triggers whenever an item is checked/unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // if we are on ordering mode
            if (btnOkChklst.Tag.ToString() == "OkOrder")
            {
                // The sum so far
                double sum = Convert.ToDouble(lblOrderPrice.Text);

                // The new price
                double checkedItemPrice = DishHandler.Instance.MenuProp.GetPriceByIndex(e.Index);

                if (e.CurrentValue == CheckState.Checked)
                    sum -= checkedItemPrice;

                else
                    sum += checkedItemPrice;

                lblOrderPrice.Text = sum.ToString();
            }
        }

        /// <summary>
        /// The Ok button of the item choosing panel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOkChklst_Click(object sender, EventArgs e)
        {
            panelChooseItems.Hide();

            if (btnOkChklst.Tag.ToString() == "OkCreateMenu")
                DishHandler.Instance.CreateMenu(chkList);

            else if (btnOkChklst.Tag.ToString() == "OkOrder")
            {
                panelPayment.Show();

                List<Dish> dishes = DishHandler.Instance.GetDishesFromACheckedList(chkList);
                OrderInfo orderInfo = new OrderInfo(dishes);
                
                OrdersDatabase.OrdersInfo.Add(orderInfo);
            }

            else if (btnOkChklst.Tag.ToString() == "OkRemoveDishes")
            {
                RemoveItems(DishHandler.Instance.Dishes);
                MessageBox.Show("Dishes removed");
            }

            else if (btnOkChklst.Tag.ToString() == "OkRemoveSupplies")
            {
                RemoveItems(SupplyHandler.Instance.Supplies);
                MessageBox.Show("Supplies removed");
            }

            else if (btnOkChklst.Tag.ToString() == "OkRemoveEmployees")
            {
                RemoveItems(EmployeeHandler.Employees);
                MessageBox.Show("Employees removed");
            }
        }

        private void btnCancelChklst_Click(object sender, EventArgs e)
        {
            btnCancelChklst.Parent.Hide();
        }
        #endregion

        #region Menu
        /// <summary>
        /// Creating a new menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createStripItem_Click(object sender, EventArgs e)
        {
            if (DishHandler.Instance.Dishes.Count == 0) { MessageBox.Show("No dishes were added to the kitchen yet"); return; }

            chkList.Items.Clear();

            // Populating chkListDishes with the DishHandler's Dishes list
            foreach (Dish dish in DishHandler.Instance.Dishes)
                chkList.Items.Add(dish.ToString());

            panelChooseItems.Show();

            btnOkChklst.Tag = "OkCreateMenu";

            PriceLblsShow(false);
        }

        /// <summary>
        /// View Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DishHandler.Instance.MenuExists) { MessageBox.Show("You didn't create a menu"); return; }

            listBox.Items.Clear();

            foreach (Dish dish in DishHandler.Instance.MenuProp.Dishes)
                listBox.Items.Add(dish.ToString());

            panelView.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag.ToString() == "1")
                HidePanels();
        }
        #endregion

        #region RemoveStripItemsClick
        private void removeDishesStripItem_Click(object sender, EventArgs e)
        {
            PriceLblsShow(false);

            if (DishHandler.Instance.Dishes.Count == 0) { MessageBox.Show("No dishes to remove"); return; }

            panelChooseItems.Show();

            View.PopulateChklstFromList(DishHandler.Instance.Dishes, ref chkList);

            btnOkChklst.Tag = "OkRemoveDishes";
        }

        private void removeSuppliesStripItem_Click(object sender, EventArgs e)
        {
            PriceLblsShow(false);

            if (SupplyHandler.Instance.Supplies.Count == 0) { MessageBox.Show("No supplies to remove"); return; }

            panelChooseItems.Show();

            chkList.Items.Clear();

            // Populating chkList with the supplies
            foreach (Supply supply in SupplyHandler.Instance.Supplies)
                chkList.Items.Add(supply.ToString());

            btnOkChklst.Tag = "OkRemoveSupplies";
        }

        private void removeEmployeesStripItem_Click(object sender, EventArgs e)
        {
            PriceLblsShow(false);

            if (EmployeeHandler.Employees.Count == 0) { MessageBox.Show("No employees to remove"); return; }

            panelChooseItems.Show();

            View.PopulateChklstFromList(EmployeeHandler.Employees, ref chkList);

            btnOkChklst.Tag = "OkRemoveEmployees";
        }

        private void RemoveItems<T>(List<T> items)
        {
            for (int i = items.Count - 1; i >= 0; i--) // Removing top-down or else the indices higher than the one being removed would chanage during a removal.
                if (chkList.GetItemChecked(i))
                    items.RemoveAt(i);

            panelChooseItems.Hide();
        }

        #endregion  

        #region TableStripItems
        private void viewTablesInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableHandler.Instance.PrintTableStates();
        }

        private void occupyTableStripItem_Click(object sender, EventArgs e)
        {
            int i = TableHandler.Instance.GetTableNumFromUser();

            if (i >= 1 && i <= 6)
                TableHandler.Instance.ChangeTableOccupationState(i, true);
        }

        private void freeATableStripItem_Click(object sender, EventArgs e)
        {
            int i = TableHandler.Instance.GetTableNumFromUser();

            if (i >= 1 && i <= 6)
                TableHandler.Instance.ChangeTableOccupationState(i, false);
        }

        private void chairsInATableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = TableHandler.Instance.GetTableNumFromUser();

            if (i >= 1 && i <= 6)
                TableHandler.Instance.ChangeChairsInTable(i);
        }
        #endregion

        #region OkBtnsClickAddEdit
        private void btnOkAddEditDish_Click(object sender, EventArgs e)
        {
            string info = ""; // Will hold the checkboxes' texts

            // editIndex will be -1 if we are adding a dish, and will hold the edited row if we edit.
            int editIndex = panelAddEditDish.Tag.ToString() == "" ? -1 : Convert.ToInt32(panelAddEditDish.Tag.ToString());

            foreach (CheckBox c in panelAddEditDish.Controls.OfType<CheckBox>())
                if (c.Checked)
                    info += c.Text + ", ";

            info = info.TrimEnd(' ');
            info = info.TrimEnd(',');

            DishHandler.Instance.AddDish(txtBoxDishName.Text, txtBoxDishID.Text, cbType.SelectedItem.ToString(), info, txtBoxDishPrice.Text, editIndex, out bool res);

            ResetSelections(ref panelAddEditDish);

            if (!res) return;

            if (editIndex == -1)
                MessageBox.Show("Dish added to the Kitchen.");
            else
                MessageBox.Show("Dish edited successfully");
        }

        private void btnOkAddEditSupply_Click(object sender, EventArgs e)
        {
            int editIndex = panelAddEditSupply.Tag.ToString() == "" ? -1 : Convert.ToInt32(panelAddEditSupply.Tag.ToString());

            SupplyHandler.Instance.AddSupply(txtBoxSupplyName.Text, txtBoxSupplyWeight.Text, txtBoxSupplyTemp.Text, txtBoxSupplyPriceForKilo.Text, editIndex, out bool res);

            ResetSelections(ref panelAddEditSupply);

            if (!res) return;

            if (editIndex == -1)
                MessageBox.Show("Supply added to the Kitchen.");
            else
                MessageBox.Show("Supply edited successfully");
        }

        private void btnOkAddEditEmployee_Click(object sender, EventArgs e)
        {

            int editIndex = panelAddEditEmployee.Tag.ToString() == "" ? -1 : Convert.ToInt32(panelAddEditEmployee.Tag.ToString());

            EmployeeHandler.AddEmployee(txtBoxEmployeeName.Text, txtBoxEmployeeId.Text, txtBoxEmployeeAge.Text, txtBoxEmployeePosition.Text, txtBoxEmployeeSalary.Text, editIndex, out bool res);

            ResetSelections(ref panelAddEditEmployee);

            if (!res) return;

            if (editIndex == -1)
                MessageBox.Show("Employee added successfully");
            else
                MessageBox.Show("Employee edited successfully");
        }
        #endregion

        #region CancelBtnsClickAddEdit
        private void btnCancelAddEditDish_Click(object sender, EventArgs e)
        {
            btnCancelAddEditDish.Parent.Hide();
        }

        private void btnCancelAddEditSupply_Click(object sender, EventArgs e)
        {
            panelAddEditSupply.Hide();
        }

        private void btnCancelAddEditEmployee_Click(object sender, EventArgs e)
        {
            panelAddEditEmployee.Hide();
        }
        #endregion

        #region AddStripItemsClick
        private void addDishStripItem_Click(object sender, EventArgs e)
        {
            panelAddEditDish.Show();
        }

        private void addSupplyStripItem_Click(object sender, EventArgs e)
        {
            panelAddEditSupply.Show();
        }

        private void addEmployeeStripItem_Click(object sender, EventArgs e)
        {
            panelAddEditEmployee.Show();
        }
        #endregion

        #region EditStripItemsClick
        private void editDishStripItem_Click(object sender, EventArgs e)
        {
            ViewItems(DishHandler.Instance.Dishes);
            btnOkView.Tag = "OkEditDish";
        }

        private void editSupplyStripItem_Click(object sender, EventArgs e)
        {
            ViewItems(SupplyHandler.Instance.Supplies);
            btnOkView.Tag = "OkEditSupply";
        }

        private void editEmployeeStripItem_Click(object sender, EventArgs e)
        {
            ViewItems(EmployeeHandler.Employees);
            btnOkView.Tag = "OkEditEmployee";
        }
        #endregion


        private void aboutStripItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This bakery was founded in 1000BC");
        }

        private void MembershipInfoStripItem_Click(object sender, EventArgs e)
        {
            txtBoxinfo.Show();
            btnInfoX.Show();
        }

        private void btnInfoX_Click(object sender, EventArgs e)
        {
            txtBoxinfo.Hide();
            btnInfoX.Hide();
        }

        private void btnOrdersInfoX_Click(object sender, EventArgs e)
        {
            txtBoxOrdersInfo.Hide();
            btnOrdersInfoX.Hide();
        }
    }
}