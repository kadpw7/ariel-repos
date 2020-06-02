using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery
{
    /// <summary>
    /// The implementor interface of the payment possibilities (Cash/Credit). 
    /// </summary>
    interface IPaymentType
    {
        void Show(string message);
    }
}
