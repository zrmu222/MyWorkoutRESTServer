using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

using myworkout.model.service.exceptions.UserFileException;
using LibMyWorkout.Domain;

namespace myworkout.model.service.databaseService
{
	public class SerializationSvcImpl 
	{
		//string directory = "/Users/zacharymurphy/Projects/MyWorkout/MyWorkout/data/";
        string directory = "data/";
		string file = "userList.txt";


		public User getUser(int id)
		{
			Console.WriteLine("Starting getUser service");
			Dictionary<int, User> userList = getUserList();
			User user = null;
			try
			{
				user = userList[id];
			}
			catch (KeyNotFoundException knf)
			{
				throw new UserFileException("Serialization : getUser : userId not found. " + knf.Message);
			}
			catch (Exception ex)
			{
				throw new UserFileException("Serialization : getUser : " + ex.Message);
			}

			return user;
		}//End of getUser

		public bool deleteUser(int id)
		{
			Console.WriteLine("Starting deleteUser service");
			Dictionary<int, User> userList = getUserList();
			bool deleted = false;
			try
			{
				userList.Remove(id);
				deleted = true;
			}
			catch (KeyNotFoundException knf)
			{
				throw new UserFileException("Serialization : deleteUser : userId not found. " + knf.Message);
			}
			catch (Exception ex)
			{
				throw new UserFileException("Serialization : deleteUser : " + ex.Message);
			}

			return deleted;
		}//End of deleteUser




		public User saveUser(User user)
		{
			Console.WriteLine("Starting saveUser service");
			Dictionary<int, User> userList = getUserList();

			if (user.UserId != 0)
				{
				int userId = user.UserId;
				userList.Remove(userId);
				userList.Add(user.UserId, user);
			}
			else 
			{
				int maxId = 0;
				foreach (KeyValuePair<int, User> users in userList)
				{
					if (users.Key > maxId)
					{
						maxId = users.Key;
					}
				}
				user.UserId = maxId + 1;
				userList.Add(user.UserId, user);
			}

			saveUserList(userList);

			return user;
		}//End of saveUser

       

		private Dictionary<int, User> getUserList()
		{
            /*
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
				File.Create(directory + file);
			}else if(!File.Exists(directory + file))
			{
				File.Create(directory + file);
			}
			if (new FileInfo(directory + file).Length == 0)
			{
				Dictionary<int, User> userList1 = new Dictionary<int, User>();
				userList1.Add(0, new User());
				saveUserList(userList1);
			}

             */


			Dictionary<int, User> userList = null;
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(directory + file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
				IFormatter format = new BinaryFormatter();
				userList = format.Deserialize(fileStream) as Dictionary<int, User>;
			}
			catch (FileNotFoundException fnf)
			{
				throw new UserFileException("SerializationSvc : getUserList : " + fnf.Message);
			}
			catch (Exception ex)
			{
				throw new UserFileException("Serialization : getUserList : " + ex.Message);
			}
			finally
			{
				try
				{
					fileStream.Close();
				}
				catch (Exception ex)
				{
					throw new UserFileException("Serialization : getUserList : " +
					                            "Problem Closing the file. " + ex.Message);
				}
			}

			return userList;
		}//End of getUserList


		private bool saveUserList(Dictionary<int, User> userList)
		{
			FileStream fileStream = null;
			bool saved = false;
			try
			{
				fileStream = new FileStream(directory + file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
				IFormatter format = new BinaryFormatter();
				format.Serialize(fileStream, userList);
				saved = true;
			}
			catch (FileLoadException fle)
			{
				throw new UserFileException("SerializationSvc : saveUserList : " + fle.Message);
			}
			catch (FileNotFoundException fnf)
			{
				throw new UserFileException("SerializationSvc : saveUserList : " + fnf.Message);
			}
			catch (Exception ex)
			{
				throw new UserFileException("Serialization : saveUserList : " + ex.Message);
			}
			finally
			{
				try
				{
					fileStream.Flush();
					fileStream.Close();
				}
				catch (Exception ex)
				{
					throw new UserFileException("Serialization : saveUserList : Problem closing File. " + ex.Message);
				}
			}

			return saved;
		}//End of saveUserList





	}//End of class
}//End of namespace
