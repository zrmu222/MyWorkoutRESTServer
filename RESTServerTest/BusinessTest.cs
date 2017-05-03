using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myworkout.model.business;
using LibMyWorkout.Domain;

namespace RESTServerTest
{
    [TestClass]
    public class BusinessTest
    {
        UserMgr manager;


        [TestInitialize]
        public void SetUp()
        {
            manager = new UserMgr();

        }



        [TestMethod]
        public void createUser()
        {
            Console.WriteLine("Starting createUser Test");

            User user = manager.createUser();
            Console.WriteLine("User: " + user.ToString());
            Assert.IsTrue(user.validate());

        }

        [TestMethod]
        public void createAdmin()
        {
            Console.WriteLine("Starting to create admin");
            User user = manager.createUser();
            user.FirstName = "Admin";
            user.LastName = "Admin";
            user.UserName = "Admin";
            user.Role = "Admin";
            User u = manager.saveNewUser(user);
            Console.WriteLine("User: " + u.ToString());
            Assert.IsTrue(u.validate());
        }

        [TestMethod]
        public void getAdmin()
        {
            Console.WriteLine("Starting test to get admin");
            User user = manager.getUser("Admin", "Password");
            Console.WriteLine("User: " + user.ToString());
            Assert.IsTrue(user.validate());
        }
    }
}
