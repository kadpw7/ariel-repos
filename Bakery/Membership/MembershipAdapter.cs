using System;

namespace Bakery
{
    class MembershipAdapter : Membership // Using the adapter design pattern to easily handle different customer types.
    {
        private MembershipData _data;

        public MembershipAdapter(string name)
          : base(name)
        {
        }

        public override string Display()
        {
            _data = new MembershipData(); // Adaptee

            discountInPrecents = _data.GetDiscountInPrecents(name);
            paymentsOption = _data.GetPaymentsOption(name);
            birthdayDish = _data.GetBirthdayDish(name);

            return string.Format("{0}{1}Discount: {2}%{1}Max payments: {3}{1}Birthday free dish: {4}{1}{1}", base.Display(), Environment.NewLine, discountInPrecents, paymentsOption, birthdayDish);
        }
    }
}
