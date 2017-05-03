using System;
namespace myworkout.model.service.exceptions.UserFileException
{
	public class UserFileException : Exception
	{
		public UserFileException(string s) : base(s)
		{
			Console.WriteLine(s);
		}
	}
}
