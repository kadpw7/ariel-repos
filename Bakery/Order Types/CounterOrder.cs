using System;
using System.Linq;
using System.Windows.Forms;

namespace Bakery.Order_Types
{
    /// <summary>
    /// The class representing counter orders
    /// </summary>
    class CounterOrder : Order
    {
        public override void Show()
        {
            paymentType.Show("Counter");
        }
    }
}
