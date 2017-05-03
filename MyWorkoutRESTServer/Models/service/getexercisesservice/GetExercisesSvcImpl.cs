using System;
using System.Collections.Generic;
using myworkout.model.service.exceptions.InvalidUserException;

using LibMyWorkout.Domain;


namespace myworkout.model.service.getExercisesService
{
	public class GetExercisesSvcImpl : IGetExercisesService 
	{

		User user;

		public IList<Exercise> getExercises(User user)
		{
			Console.WriteLine("Starting GetExercise Service");

			IList<int> exNumbers = null;
			IList<Exercise> exerciseList = null;

			try
			{
				this.user = user;
				exNumbers = getIntList();
                Console.WriteLine(exNumbers.ToString());
				exerciseList = getList(exNumbers);
			}
			catch (NullReferenceException nre)
			{
				throw new InvalidUserException("GetExercisesSvc : User Weeks, Days, or Exercises are null. " + nre.Message); 
			}


			return exerciseList;
		}//End of getExercises




		private IList<int> getIntList()
		{
			IList<int> intList = new List<int>();

			foreach (Week wk in user.Weeks)
			{
				if (wk.WeekOrderNumber == user.CurrentWeek)
				{
					foreach (Day day in wk.Days)
					{
						if (day.DayOrderNumber == user.CurrentDay)
						{
							intList = day.ExerciseListNumber;
						}
					}
				}
			}


			return intList;
		}//end of getIntList





		private IList<Exercise> getList(IList<int> exerciseNumber)
		{
			IList<Exercise> exerciseList = new List<Exercise>();

			foreach (int i in exerciseNumber)
			{
				Exercise ex = getExercise(i);
				exerciseList.Add(ex);
			}

			return exerciseList;


		}//end of getList


		private Exercise getExercise(int x)
		{
			Exercise ex = new Exercise();

			foreach (Exercise exercise in user.ExerciseList)
			{
				if (exercise.ExerciseNumber == x)
				{
					ex = exercise;
				}
			}


			return ex;
		}//end of getExercise

	}
}
