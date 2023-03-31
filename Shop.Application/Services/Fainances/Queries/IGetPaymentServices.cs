using Shop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Fainances.Queries
{
    public interface IGetPaymentServices
    {
        ResultDto<GetPaymentDto> GetPayment(Guid guid);
    }
}
