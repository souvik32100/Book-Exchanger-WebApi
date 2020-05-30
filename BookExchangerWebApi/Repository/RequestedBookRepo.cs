using BookExchangerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookExchangerWebApi.Repository
{
    public class RequestedBookRepo : Repository<RequestedBook>, IRequestedBookRepo
    {

        BookExchangerDBEntities context;

        public RequestedBookRepo()
        {
            context = new BookExchangerDBEntities();
        }
        public IEnumerable<RequestedBook> GetReqBooksUserId(int userId)
        {
            return context.RequestedBooks.Where(x=>x.UserId==userId);
        }
    }
}