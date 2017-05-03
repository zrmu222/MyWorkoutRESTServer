using System;
namespace myworkout.model.service.exceptions.InvalidUserException
{
	public class InvalidUserException : Exception
	{
		public InvalidUserException(string s) : base(s)
		{
			Console.WriteLine(s);
		}


	}
}
