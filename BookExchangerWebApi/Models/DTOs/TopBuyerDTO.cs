﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Models.DTOs
{
    public class TopBuyerDTO
    {
        public string Email { get; set; }
        public int Count { get; set; }
    }
}