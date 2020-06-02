using System;
using System.Linq;
using System.Windows.Forms;

namespace Bakery.Order_Types
{
    /// <summary>
    /// The class representing phone orders
    /// </summary>
    class PhoneOrder : Order
    {
        public override void Show()
        {
            paymentType.Show("Phone");
        }
    }
}