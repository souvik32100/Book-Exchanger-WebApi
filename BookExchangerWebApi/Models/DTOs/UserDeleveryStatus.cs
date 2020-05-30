using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Models.DTOs
{
    public class UserDeleveryStatus
    {
        public string BookName { get; set; }
        public string SellerName { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime DeleveryDate { get; set; }
        public int Status { get; set; }
    }
}