using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Bakery
{
    public static class View
    {
        /// <summary>
        /// An Ok-Cancel popup window
        /// </summary>
        /// <param name="title"></param>
        /// <param name="promptText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        /// <summary>
        /// Displaying a string array in a listbox.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static void DisplayListBox(string[] text)
        {
            Form form = new Form();
            ListBox listBox = new ListBox();
            Button buttonOk = new Button();

            foreach (string st in text)
                listBox.Items.Add(st);

            buttonOk.Text = "OK";

            listBox.SetBounds(12, 36, 372, 300);
            buttonOk.SetBounds(228, 72, 75, 23);

            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            buttonOk.DialogResult = DialogResult.OK;
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { listBox, buttonOk });
            form.ClientSize = new Size(Math.Max(300, listBox.Right + 10), form.ClientSize.Height);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            form.Width = 400;
            form.Height = 400;

            DialogResult dialogResult = form.ShowDialog();
        }

        /// <summary>
        /// For inputing a text in a popup window
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="oldInput"></param>
        /// <returns></returns>
        public static string InputTxt(string title, string text, string oldInput)
        {
            string newInput = "";

            if (InputBox(title, text, ref newInput) == DialogResult.OK)
                return newInput;

            else
                return oldInput;
        }

        public static void PanelInit()
        {
            foreach (Panel p in Application.OpenForms["Form1"].Controls.OfType<Panel>())
            {
                p.Size = new Size(400, 350);
                p.Location = new Point(240, 120);
                p.Hide();
                p.Tag = "";
            }
        }

        public static void ListBoxInit(ListBox listBox)
        {
            listBox.BackColor = Application.OpenForms["Form1"].BackColor;
            listBox.Size = new Size(400, 250);
            listBox.Parent.Size = new Size(400, 400);
            listBox.Location = new Point(40, 40);
        }

        public static void PopulateChklstFromList<T>(List<T> list, ref CheckedListBox chkLst)
        {
            chkLst.Items.Clear();

            // Populating chkListDishes with the dishes
            foreach (var item in list)
                chkLst.Items.Add(item.ToString());
        }

        public static void OrdersInfoMenuInit(ref TextBox t, ref Button b)
        {
            t.Location = new Point(350, 200);
            t.Width = 300;
            t.Height = 200;
            b.Location = new Point(320 + t.Width, 180);
            t.Hide();
            b.Hide();

        }



        public static void InfoMenuInit(ref TextBox t, ref Button b)
        {
            t.Location = new Point(350, 200);
            t.Width = 300;
            t.Height = 200;
            b.Location = new Point(320 + t.Width, 180);
            t.Hide();
            b.Hide();

            Membership regular = new MembershipAdapter("Regular");
            Membership active = new MembershipAdapter("Active");
            Membership loyal = new MembershipAdapter("Loyal");
            Membership premium = new MembershipAdapter("Premium");

            t.Text = regular.Display() + active.Display() + loyal.Display() + premium.Display();
        }
    }
}
