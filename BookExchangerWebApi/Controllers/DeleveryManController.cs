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
    [RoutePrefix("api/deleverymen")]
    //[BasicAuthentication]
    public class DeleveryManController : ApiController
    {
        private readonly IDeleveryManRepo repo;

        public DeleveryManController()
        {
            repo = new DeleveryManRepo();
        }
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var sel = repo.GetAll();
            List<DeleveryMan> u = new List<DeleveryMan>();
            foreach (var b in sel)
            {
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen", Method = "GET", Rel = "Get all the user list" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen", Method = "POST", Rel = "Create a new user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen/" + b.DeleveryManId, Method = "PUT", Rel = "Modify an existing user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen/" + b.DeleveryManId, Method = "DELETE", Rel = "Delete an existing user resource" });

                u.Add(b);

            }



            return Ok(u);
        }


        [Route("{id}", Name = "GetDeleveryManById")]
        public IHttpActionResult Get(int id)
        {
            var deleveryMan = repo.Get(id);
            DeleveryMan b = deleveryMan;
            if (deleveryMan != null)
            {

                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen", Method = "GET", Rel = "Get all the user list" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen", Method = "POST", Rel = "Create a new user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen/" + b.DeleveryManId, Method = "PUT", Rel = "Modify an existing user resource" });
                b.Links.Add(new Links() { HRef = "http://localhost:62832/api/deleverymen/" + b.DeleveryManId, Method = "DELETE", Rel = "Delete an existing user resource" });

                return Ok(b);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]DeleveryMan deleveryMan)
        {
            try
            {

                deleveryMan.DeleveryManId = id;
                repo.Update(deleveryMan);
                return Ok(deleveryMan);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("")]
        public IHttpActionResult Post([FromBody]DeleveryMan deleveryMan)
        {
            try
            {
                repo.Insert(deleveryMan);
                string url = Url.Link("GetDeleveryManById", new { id = deleveryMan.DeleveryManId });
                return Created(url, deleveryMan);
            }
            catch (Exception e)
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
        #region Zahir

        [HttpGet]
        [Route("total")]
        public IHttpActionResult TotalDeliveryMan()
        {
            return Ok(repo.TotalDeliveryMan());
        }


        [HttpDelete]
        [Route("email/{mail}")]
        public IHttpActionResult DeleteByEmail(string mail)
        {
            repo.DeleteByEmail(mail);
            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion


        #region turash

        

        [Route("SellPostingsForDelevery/{sid}")]
        public IHttpActionResult Put([FromUri]int sid, [FromBody]SellPosting sellPosting)
        {
            try
            {
                SellPostingRepo repo = new SellPostingRepo();

                sellPosting.SellId = sid;

                repo.Update(sellPosting);
                return Ok(sellPosting);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        
        

        [Route("SellPostingsForDelevery/{sid}")]
        public IHttpActionResult Post([FromUri]int id, [FromUri]int sid, [FromBody]DeleveryDetail deleveryDetail)
        {
            try
            {
                IDeleveryDetailsRepo repo = new DeleveryDetailsRepo();
                deleveryDetail.DeleveryManId = id;
                deleveryDetail.SellId = sid;
                deleveryDetail.BookReceivedDate = DateTime.Now;
                deleveryDetail.BookDeleverdDate = DateTime.Now;
                repo.Insert(deleveryDetail);
                string url = Url.Link("GetDeleveryById", new { id = deleveryDetail.DeleveryManId });
                return Created(url, deleveryDetail);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
        #endregion



        #region turash

        [Route("SellPostingsForDelevery")]
        public IHttpActionResult GetSells()
        {
            ISellPostingRepo sellpost = new SellPostingRepo();
            return Ok(sellpost.PendingList().ToList());
        }

        [Route("SellPostingsForDelevery/{sid}")]
        public IHttpActionResult PutDelevery([FromUri]int sid, [FromBody]SellPosting sellPosting)
        {
            try
            {
                SellPostingRepo repo = new SellPostingRepo();

                sellPosting.SellId = sid;

                repo.Update(sellPosting);
                return Ok(sellPosting);

            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [Route("{id}/deleverydetails")]
        public IHttpActionResult GetDeliveryDtail(int id)
        {
            IDeleveryDetailsRepo ddtails = new DeleveryDetailsRepo();

            var delevery = ddtails.GetDeliveryByID(id);
            if (delevery != null)
            {
                return Ok(delevery);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }

        [Route("{id}/deleverydetails/{sid}")]
        public IHttpActionResult Postss([FromUri]int id, [FromUri]int sid, [FromBody]DeleveryDetail deleveryDetail)
        {
            try
            {
                IDeleveryDetailsRepo repo = new DeleveryDetailsRepo();
                deleveryDetail.DeleveryManId = id;
                deleveryDetail.SellId = sid;
                deleveryDetail.BookReceivedDate = DateTime.Now;
                deleveryDetail.BookDeleverdDate = DateTime.Now;
                repo.Insert(deleveryDetail);
                string url = Url.Link("GetDeleveryById", new { id = deleveryDetail.DeleveryManId });
                return Created(url, deleveryDetail);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [Route("Alldeleverydetails")]
        public IHttpActionResult GetAllDeliveryDtail()
        {
            IDeleveryDetailsRepo ddtails = new DeleveryDetailsRepo();

            var delevery = ddtails.GetAll();
            if (delevery != null)
            {
                return Ok(delevery);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

        }
        #endregion
    }
}
