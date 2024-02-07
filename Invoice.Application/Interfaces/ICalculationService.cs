using Invoice.DataTransferObject.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Interfaces
{
    public interface ICalculationService
    {
        public Task<Result<double>> CalculateNetAfterTax(double tax, double total);
        public Task<Result<double>> CalculateNetAfterDiscount(double discount, double total);
    }
}
