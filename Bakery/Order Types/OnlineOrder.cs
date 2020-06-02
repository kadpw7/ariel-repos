using System;
using System.Linq;
using System.Windows.Forms;

namespace Bakery.Order_Types
{
    /// <summary>
    /// The class representing online orders
    /// </summary>
    class OnlineOrder : Order
    {
        public override void Show()
        {
            paymentType.Show("Online");
        }
    }
}
