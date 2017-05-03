using System;
using System.Collections;
using System.Collections.Generic;

using LibMyWorkout.Domain;


namespace myworkout.model.service.newUserSetupService
{
	public class NewUserSvcImpl : INewUserSetUpService
	{

		public User newUserSetUp()
		{
			Console.WriteLine("Starting newUserSetup service");
			User user = setUp();
			return user;
		}//end of newUserSetUp




		private User setUp()
		{
			Week wk1 = new Week(1, 1, getWK1Days());
			Week wk2 = new Week(2, 2, getWK2Days());
			IList<Week> weeks = new List<Week>();
			weeks.Add(wk1);
			weeks.Add(wk2);
			User user = new User(1, 1, weeks, getExerciseList());
            user.Role = "User";
			return user;
		}//end of user setup;


		private List<Exercise> getExerciseList()
		{
			List<Exercise> exerciseList = new List<Exercise>();

			Exercise overHeadPress = new Exercise("Overhead Press", 1, 45.0);
			Exercise benchPress = new Exercise("Bench Press", 2, 140.0);
			Exercise chinUps = new Exercise("Chinups", 3, 0.0);
			Exercise barbellRows = new Exercise("Barbell Rows", 4, 100.0);
			Exercise squats = new Exercise("Squats", 5, 280.0);
			Exercise deadLift = new Exercise("Deadlifts", 6, 300.0);

			exerciseList.Add(overHeadPress);
			exerciseList.Add(benchPress);
			exerciseList.Add(chinUps);
			exerciseList.Add(barbellRows);
			exerciseList.Add(squats);
			exerciseList.Add(deadLift);

			return exerciseList;
		}



		public IList<Day> getWK1Days()
		{
			IList<Day> days = new List<Day>();
			IList<int> exerciseNumber = new List<int>();
			exerciseNumber.Add(1);
			exerciseNumber.Add(3);
			exerciseNumber.Add(5);
			Day day1 = new Day(1, 1, exerciseNumber);

			exerciseNumber = new List<int>();
			exerciseNumber.Add(2);
			exerciseNumber.Add(4);
			exerciseNumber.Add(6);
			Day day2 = new Day(2, 2, exerciseNumber);

			exerciseNumber = new List<int>();
			exerciseNumber.Add(1);
			exerciseNumber.Add(3);
			exerciseNumber.Add(5);
			Day day3 = new Day(3, 3, exerciseNumber);

			days.Add(day1);
			days.Add(day2);
			days.Add(day3);

			return days;
		}

		public IList<Day> getWK2Days()
		{
			IList<Day> days = new List<Day>();
			IList<int> exerciseNumber = new List<int>();
			exerciseNumber.Add(2);
			exerciseNumber.Add(4);
			exerciseNumber.Add(5);
			Day day1 = new Day(4, 1, exerciseNumber);

			exerciseNumber = new List<int>();
			exerciseNumber.Add(1);
			exerciseNumber.Add(3);
			exerciseNumber.Add(6);
			Day day2 = new Day(5, 2, exerciseNumber);

			exerciseNumber = new List<int>();
			exerciseNumber.Add(2);
			exerciseNumber.Add(4);
			exerciseNumber.Add(5);
			Day day3 = new Day(6, 3, exerciseNumber);

			days.Add(day1);
			days.Add(day2);
			days.Add(day3);

			return days;
		}


	}//end of class
}
