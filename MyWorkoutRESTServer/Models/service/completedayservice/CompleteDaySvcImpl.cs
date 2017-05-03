using System;
using System.Collections;

using LibMyWorkout.Domain;
using myworkout.model.service.exceptions.InvalidUserException;


namespace myworkout.model.service.completeDayService
{
	public class CompleteDaySvcImpl : ICompleteDayService
	{
		User user;


		public User completeDay(User user, Hashtable hashTable)
		{
			Console.WriteLine("Starting completeDay service");

			try
			{
				this.user = user;
				updateDay(hashTable);
			}
			catch (NullReferenceException nre)
			{
				throw new InvalidUserException("CompleteDaySvc : user is not valid. " + nre.Message);
			}
			return user; 
		}


		private void updateDay(Hashtable hashTable)
		{
			//Loops through the hashTable to get the exerciseNumber and lastSetReps
			foreach (DictionaryEntry pair in hashTable)
			{
				int exerciseNumber = (int)pair.Key;
				int lastSetReps = (int)pair.Value;
				//Calls the method to update the user exercise weight based off their lastSetReps
				updateExerciseWeight(exerciseNumber, lastSetReps);
			}//end of foreach

			//Calls method to update the dayNumber in Days inside Week inside Weeks
			updateDayNumber();


			//Function to check to see if it is the end of the week or not.
			//If not end of week it adds 1 to the currentDay number
			if (user.CurrentDay < 3)
			{
				user.CurrentDay += 1;
			}
			//if it is then i changes dayNumber to 1. and changed week to 1 or 2 based on current weekNumber
			else {
				//Since it is the end of th week we need to update the weekNumber inside the week object
				updateWeekNumber();
				user.CurrentDay = 1;
				if (user.CurrentWeek == 1)
				{
					user.CurrentWeek = 2;
				}
				else 
				{
					user.CurrentWeek = 1;
				}
			}//end of if/else
		}//end of updateDay



		//Method to update the users exercise weight based off the lastSetReps
		//if you donot reach 5 reps you download weight by 10%
		//Upperbody exercises(1-4) go up by 2.5lbs
		//Lower body exercise(5 and 6) go up by 5lbs
		//if you make more than 10 reps you go up double
		private void updateExerciseWeight(int exerciseNumber, int lastSetReps)
		{

			foreach (Exercise ex in user.ExerciseList)
			{

				if (ex.ExerciseNumber == exerciseNumber)
				{
					if (lastSetReps < 5)
					{
						ex.Weight = (ex.Weight) - (ex.Weight * .10);
					}
					else if (lastSetReps >= 10)
					{
						if (ex.ExerciseNumber < 5)
						{
							ex.Weight += 5.0;
						}
						else 
						{
							ex.Weight += 10.0;
						}

					}
					else {
						if (ex.ExerciseNumber < 5)
						{
							ex.Weight += 2.5;
						}
						else 
						{
							ex.Weight += 5.0;
						}
					} //end of if, else if, and else

				}//end of if if

			}//end of foreach
		}//end of getExercise


		private void updateDayNumber()
		{
			foreach (Week wk in user.Weeks)
			{
				if (wk.WeekOrderNumber == user.CurrentWeek)
				{
					foreach (Day day in wk.Days)
					{
						if (day.DayOrderNumber == user.CurrentDay)
						{
							day.DayNumber += 6;
						}
					}//end foreach day
				}//end if wk
			}//end foreach week

		}//end of updateDayNumber



		private void updateWeekNumber()
		{
			foreach (Week wk in user.Weeks)
			{
				if (wk.WeekOrderNumber == user.CurrentWeek)
				{
					wk.WeekNumber += 2;
				}//end of if
			} //end of foreach
		}//end of updateWeekNumber



	}//end of class
}//end of namespace
