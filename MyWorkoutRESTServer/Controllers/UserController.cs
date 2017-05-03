using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using LibMyWorkout.Domain;
using myworkout.model.business;

namespace MyWorkoutRESTServer.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public ArrayList Get()
        {
            UserMgr manager = new UserMgr();
            ArrayList userList = manager.getUsers();

            return userList;
        }

        // GET: api/User/5
        public User Get(int id)
        {
            UserMgr manager = new UserMgr();
            User user = manager.createUser();

            return user;
        }

        
        // GET: api/User/5/1234
        [Route("api/User/{userName}/{password}")]
        public User Get(string userName, string password)
        {
            UserMgr manager = new UserMgr();
            User user = manager.getUser(userName, password);
            return user;
        }
        
        //Create User
        // POST: api/User
        public HttpResponseMessage Post([FromBody]User value)
        {
            string redirect = "";
            HttpResponseMessage responce = null;
            UserMgr manager = new UserMgr();
            bool userNameTaken = manager.checkUserName(value.UserName);
            if (!userNameTaken)
            {
                User user = manager.createUser();
                user.FirstName = value.FirstName;
                user.LastName = value.LastName;
                user.UserName = value.UserName;
                user.Password = value.Password;
                User u = manager.saveNewUser(user);
                if (u.validate())
                {
                    redirect = "User/" + u.UserName + "/" + u.Password + "/";
                    responce = Request.CreateResponse(HttpStatusCode.Created);
                    responce.Headers.Location = new Uri(Request.RequestUri, redirect);
                }
                else
                {
                    responce = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }

            }
            else
            {
                responce = Request.CreateResponse(HttpStatusCode.Conflict);
            }


            return responce;
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
