using Invoice.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Test
{
    public class DiscountCalculationUnitTest
    {
        [Fact]
        public void CalculateNetAfterDiscount_NullInput_return_0_And_notError()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterDiscount(0, 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void CalculateNetAfterDiscount_ZerDiscountWithTotal_ReturnKeepTotal()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterDiscount(0, 100);
            Assert.NotNull(result);
        }
        [Fact]
        public void CalculateNetAfterDiscount_ZeroTotalWithDiscount_ReturnTotalZero()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterDiscount(100, 0);
            Assert.NotNull(result);
        }
        [Fact]
        public void CalculateNetAfterDiscount_TotalWithTax_ReturnTotalaAfterCalc()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterDiscount(10, 100);
            Assert.Equal(90, result.Result.Value);
        }
    }
}
