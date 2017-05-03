using System;
using System.Collections.Generic;
using System.Collections;

using LibMyWorkout.Domain;
using myworkout.model.service.newUserSetupService;
using myworkout.model.service.databaseService;
using myworkout.model.service.exceptions.UserFileException;


namespace myworkout.model.business
{
	public class UserMgr : Manager
	{

		public User createUser()
		{
            User user = null;
			INewUserSetUpService newUserService = (INewUserSetUpService)GetService(typeof(INewUserSetUpService).Name);
            if (newUserService is INewUserSetUpService)
            {
                user = newUserService.newUserSetUp();
            }else
            {
                Console.WriteLine("newUserService not initialized");
            }
			return user;
		}


		public User getUser(string userName, string password)
		{
			User user = null;
			try
			{
				IDatabaseService dataBaseService = (IDatabaseService)GetService(typeof(IDatabaseService).Name);
				user = dataBaseService.getUser(userName, password);

			}
			catch (UserFileException)
			{
			}

			return user;
		}



		public User saveNewUser(User u)
		{
			User user = null;
			try
			{
				IDatabaseService dataBaseService = (IDatabaseService)GetService(typeof(IDatabaseService).Name);
				user = dataBaseService.saveNewUser(u);
			}
			catch (UserFileException)
			{
			}
			return user;
		}

        public User updateUser(User u)
        {
            User user = null;
            try
            {
                IDatabaseService dataBaseService = (IDatabaseService)GetService(typeof(IDatabaseService).Name);
                user = dataBaseService.updateUser(u);
            }
            catch (UserFileException) { }

            return user;
        }


        public bool checkUserName(string userName)
        {
            bool userNameTaken = true;
            try
            {
                IDatabaseService dataBaseService = (IDatabaseService)GetService(typeof(IDatabaseService).Name);
                userNameTaken = dataBaseService.checkUserName(userName);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return userNameTaken;
        }

        public ArrayList getUsers()
        {
            ArrayList userList = new ArrayList();
            try
            {
                IDatabaseService dataBaseService = (IDatabaseService)GetService(typeof(IDatabaseService).Name);
                userList = dataBaseService.getUsers() ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return userList;
        }


        }
}
