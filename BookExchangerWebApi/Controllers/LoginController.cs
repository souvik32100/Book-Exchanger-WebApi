using BookExchangerWebApi.Attributes;
using BookExchangerWebApi.Models;
using BookExchangerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookExchangerWebApi.Controllers
{
    [RoutePrefix("api/logins")]
    
    public class LoginController : ApiController
    {
        private readonly IUsersRepo repo;
        IRepository<Login> lrepo = new LoginRepo();
        public LoginController()
        {
            repo = new UsersRepo();
        }

        [HttpGet]
        [Route("email/{mail}", Name = "GetLoginId")]
        public IHttpActionResult GetStatus(string mail)
        {
            //string x = mail + "@gmail.com";
            try
            {
                int us = repo.GetStatus(mail);

                //return Ok(mail);
                return Ok(us);
            }
            catch(Exception)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }


        [Route("")]
        public IHttpActionResult Post([FromBody]Login logins)
        {
            try
            {
                lrepo.Insert(logins);
                string url = Url.Link("GetLoginId", new { id = logins.Email });
                return Created(url, logins);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

    }
}
