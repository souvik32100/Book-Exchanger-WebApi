using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookExchangerWebApi.Repository
{
    interface IDeleveryDetailsRepo : IRepository<DeleveryDetail>
    {
        int TotalDeliveries();

        IEnumerable<DeleveryDetail> GetUserDelevery(int userId);
        IEnumerable<DeleveryDetail> GetDeliveryByID(int dmanID);
    }
}
