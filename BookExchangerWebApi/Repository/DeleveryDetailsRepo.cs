using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class DeleveryDetailsRepo : Repository<DeleveryDetail>, IDeleveryDetailsRepo
    {
        private readonly BookExchangerDBEntities context;

        public DeleveryDetailsRepo()
        {
            context = new BookExchangerDBEntities();
        }

        public IEnumerable<DeleveryDetail> GetUserDelevery(int userId)
        {
            IEnumerable<SellPosting> s = context.SellPostings.Where(x=>x.BuyerId==userId&&x.Status==2);
            List<DeleveryDetail> d = new List<DeleveryDetail>();
            foreach(var data in s)
            {
                DeleveryDetail dd = context.DeleveryDetails.FirstOrDefault(x=>x.SellId==data.SellId);

                d.Add(dd);
            }
            return d;


        }

        public int TotalDeliveries()
        {
            return context.SellPostings.Count(x => x.Status == 4);
        }
        #region Turash
        public IEnumerable<DeleveryDetail> GetDeliveryByID(int dmanID)
        {
            return context.DeleveryDetails.Where(x => x.DeleveryManId == dmanID).ToList();
        }


        #endregion
    }
}