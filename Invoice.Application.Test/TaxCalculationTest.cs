using Invoice.Application.Services;

namespace Invoice.Application.Test
{
    public class TaxCalculationTest
    {
        [Fact]
        public void CalculateNetAfterTax_NullInput_return_0_And_notError()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterTax(0, 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void CalculateNetAfterTax_ZeroTaxWithTotal_ReturnKeepTotal()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterTax(0, 100);
            Assert.NotNull(result);
        }
        [Fact]
        public void CalculateNetAfterTax_ZeroTotalWithTax_ReturnTotalZero()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterTax(100, 0);
            Assert.NotNull(result);
        }
        [Fact]
        public void CalculateNetAfterTax_TotalWithTax_ReturnTotalaAfterCalc()
        {
            var calcobj = new CalculationService();
            var result = calcobj.CalculateNetAfterTax(10, 100);
            Assert.Equal(110, result.Result.Value);
        }
    }
}