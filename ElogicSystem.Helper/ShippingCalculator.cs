using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.Helper
{
    public class ShippingCalculator
    {
        public double CalculateShippingCost(Quotation quotation, double shippingContribution)
        {
            int numberOfPanels = 0;

            foreach (Panel panel in quotation.Panels)
            {
                numberOfPanels += (int)panel.Quantity;
            }

            return ((numberOfPanels / Constants.NUMBER_OF_PANELS_PER_PALLET) * Constants.BASE_SHIPPING_COST) * shippingContribution;
        }
    }
}
