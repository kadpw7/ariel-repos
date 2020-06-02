using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery
{
    /// <summary>
    /// The base class of all the Order possibilities.
    /// </summary>
    abstract class Order
    {
        public IPaymentType paymentType;

        public abstract void Show();
    }
}
