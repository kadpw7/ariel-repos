using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bakery.Order_Types;
using Bakery.PaymentTypes;

namespace Bakery
{
    class OrderMaker
    {
        public Order MakeOrder(string type)
        {

            // I implement the Bridge DP in order to bridge between the order type and the payment type.
            switch (type)
            {
                case "counter": return new CounterOrder();
                case "phone": return new PhoneOrder();
                case "online": return new OnlineOrder();
                default: return new CounterOrder();
            }
        }

        // Using the bridge in order to define a payment type for our order.
        public IPaymentType CashOrCredit(string option)
        {
            if (option == "cash") return new CashOrder();
            else return new CreditOrder();
        }
    }
}
