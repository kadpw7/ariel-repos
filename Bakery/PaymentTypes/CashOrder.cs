using System;
using System.Linq;
using System.Windows.Forms;

namespace Bakery.PaymentTypes
{
    /// <summary>
    /// The implementation of the paying type interface by paying by cash.
    /// </summary>
    class CashOrder : IPaymentType
    {
        public void Show(string message)
        {
            MessageBox.Show(message + " order. Payed via Cash.");
        }
    }
}
