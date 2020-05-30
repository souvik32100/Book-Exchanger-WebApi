using BookExchangerWebApi.Attributes;
using BookExchangerWebApi.Models;
using BookExchangerWebApi.Models.DTOs;
using BookExchangerWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookExchangerWebApi.Controllers
{


    [RoutePrefix("api/users")]
    [BasicAuthentication]
    public class UserController : ApiController
    {
        private readonly IUsersRepo repo;
       // IRepository<User> repo = new UsersRepo();
        public UserController()
        {
            repo = new UsersRepo();
        }


        [Route("")]
        public IHttpActionResult GetAll()
        {
            var user = repo.GetAll();
            List<User> u = new List<User>();
            foreach(var data in user)
            {
                data.Links.Add(new Links() { HRef = "http://localhost:62832/api/users", Method = "GET", Rel = "Get all the user list" });
                data.Links.Add(new Links() { HRef = "http://localhost:62832/api/users", Method = "POST", Rel = "Create a new user resource" });
                data.Links.Add(new Links() { HRef = "http://localhost:62832/api/users/" + data.UserId, Method = "PUT", Rel = "Modify an existing user resource" });
                data.Links.Add(new Links() { HRef = "http://localhost:62832/api/users/" + data.UserId, Method = "DELETE", Rel = "Delete an existing user resource" });

                u.Add(data);

            }


            return Ok(u);
        }


        [Route("{id}", Name = "GetUserById")]
        public IHttpActionResult Get(int id)
        {
            var us = repo.Get(id);
            User user = us;

            if(us!=null)
            {
                user.Links.Add(new Links() { HRef = "http://localhost:62832/api/users", Method = "GET", Rel = "Get all the user list" });
                user.Links.Add(new Links() { HRef = "http://localhost:62832/api/users", Method = "POST", Rel = "Create a new user resource" });
                user.Links.Add(new Links() { HRef = "http://localhost:62832/api/users/" + us.UserId, Method = "PUT", Rel = "Modify an existing user resource" });
                user.Links.Add(new Links() { HRef = "http://localhost:62832/api/users/" + us.UserId, Method = "DELETE", Rel = "Delete an existing user resource" });
                


                return Ok(user);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            
        }

        [HttpGet]
        [Route("{id}/point")]
        public IHttpActionResult GetPoint(int id)
        {
            var us = repo.GetPoint(id);
           
                return Ok(us);
            

        }

        [Route("{id}/point")]
        public IHttpActionResult PutPoint(int id,[FromBody] IntDTO idt)
        {
            if (idt.bId != null)
            {
                foreach (var item in idt.bId)
                {
                    repo.PutPoint(id,item);
                    
                }

                return StatusCode(HttpStatusCode.Created);
            }

            return StatusCode(HttpStatusCode.NoContent);

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]User user)
        {
            try
            {
                Int32.Parse(user.PhoneNo);
                user.UserId = id;
                repo.Update(user);
                return StatusCode(HttpStatusCode.OK);

            }
            catch(Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]User user)
        {
            try
            {
                repo.Insert(user);
                string url = Url.Link("GetUserById", new { id = user.UserId });
                return Created(url, user);
            }
            catch(Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (repo.Get(id) != null)
            {
                repo.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("email/{mail}")]
        public IHttpActionResult GetUserByEmail(string mail)
        {
            //string x = mail + "@gmail.com";
            var us = repo.GetUserByEmail(mail);
            //return Ok(mail);
            if (us != null)
            {
                return Ok(us);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        #region Zahir

        [HttpGet]
        [Route("total")]
        public IHttpActionResult TotalUsers()
        {
            return Ok(repo.TotalUsers());
        }

        [HttpDelete]
        [Route("email/{mail}")]
        public IHttpActionResult DeleteByEmail(string mail)
        {
            repo.DeleteByEmail(mail);
            return StatusCode(HttpStatusCode.NoContent);
        }


        #endregion

    }
}
