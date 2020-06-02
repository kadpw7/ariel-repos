using System;

namespace Bakery
{
    class Membership
    {
        protected string name; // The membership name
        protected int discountInPrecents;
        protected int paymentsOption;
        protected string birthdayDish;

        public Membership(string name)
        {
            this.name = name;
        }

        public virtual string Display()
        {
            return name;
        }
    }
}
