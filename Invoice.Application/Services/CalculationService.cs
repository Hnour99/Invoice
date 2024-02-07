using Invoice.Application.Interfaces;
using Invoice.DataTransferObject.DTOs.Item;
using Invoice.DataTransferObject.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Services
{
    public class CalculationService : ICalculationService
    {
        public async Task<Result<double>> CalculateNetAfterDiscount(double discount, double total)
        {
            try
            {
                if (discount == 0)
                    return await Task.FromResult(Result<double>.Successful(total));

                var discountPercentage = (discount * total) / 100;

                var net = total - discountPercentage;
                return await Task.FromResult(Result<double>.Successful(net));
            }
            catch (Exception ex)
            {

                return await Task.FromResult(Result<double>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }

        public async Task<Result<double>> CalculateNetAfterTax(double tax, double total)
        {
            try
            {
                if (tax == 0)
                    return await Task.FromResult(Result<double>.Successful(total));
                var taxPercentage = tax / 100;
                var net = (total + (taxPercentage * total));
                return await Task.FromResult(Result<double>.Successful(net));
            }
            catch (Exception ex)
            {

                return await Task.FromResult(Result<double>.Failed(new Dictionary<string, string> { { "Tech01", "Internal error" } }));
            }
        }
    }
}
