using System;

namespace Bakery
{
    class MembershipData
    {
        // The databank 'legacy API'

        public int GetDiscountInPrecents(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "regular": return 0;
                case "active": return 5;
                case "loyal": return 10;
                case "premium": return 20;
                default: return 0;
            }
        }

        /// <summary>
        /// Number of payments available
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public int GetPaymentsOption(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "regular": return 2;
                case "active": return 4;
                case "loyal": return 6;
                case "premium": return 10;
                default: return 0;
            }
        }

        public string GetBirthdayDish(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "regular": return "None";
                case "active": return "Sliced bread";
                case "loyal": return "Cinnamon bun";
                case "premium": return "Challah";
                default: return "";
            }
        }
    }
}
