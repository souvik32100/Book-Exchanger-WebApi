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
    [RoutePrefix("api/requestedbooks")]
    [BasicAuthentication]
    public class RequestedBookController : ApiController{
        private readonly IRequestedBookRepo repo;

        public RequestedBookController(){
            repo = new RequestedBookRepo();
        }

        [Route("")]
        public IHttpActionResult GetAll(){
            var sel = repo.GetAll();
            List<RequestedBook> u = new List<RequestedBook>();
            foreach (var b in sel){
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks", Method = "GET", Rel = "Get all the user list" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks", Method = "POST", Rel = "Create a new user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks/" + b.RequestId, Method = "PUT", Rel = "Modify an existing user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks/" + b.RequestId, Method = "DELETE", Rel = "Delete an existing user resource" });
                u.Add(b);
            }
            return Ok(u);
        }

        [Route("{id}", Name = "GetRequestedBookById")]
        public IHttpActionResult Get(int id){
            var book = repo.Get(id);
            RequestedBook b = book;
            if (book != null){
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks", Method = "GET", Rel = "Get all the user list" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks", Method = "POST", Rel = "Create a new user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks/" + b.RequestId, Method = "PUT", Rel = "Modify an existing user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/requestedbooks/" + b.RequestId, Method = "DELETE", Rel = "Delete an existing user resource" });
                return Ok(b);
            }
            else{
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]RequestedBook book){
            try{
                book.RequestId= id;
                repo.Update(book);
                return Ok(book);
            }
            catch (Exception){
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]RequestedBook book){
            try{
                repo.Insert(book);
                string url = Url.Link("GetRequestedBookById", new { id = book.RequestId });
                return Created(url, book);
            }
            catch (Exception e){
                return Ok(e.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id){
            if (repo.Get(id) != null){
                repo.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            else{
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("user/{userId}", Name = "GetReqBooksUserId")]
        public IHttpActionResult GetbyUserId(int userId){
            var books = repo.GetReqBooksUserId(userId);
            if (books != null){
                return Ok(books);
            }
            else{
                return StatusCode(HttpStatusCode.NotFound);
            }

        }


    }
}
