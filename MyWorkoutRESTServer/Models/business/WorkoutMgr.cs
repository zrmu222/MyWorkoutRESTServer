using System.Collections.Generic;
using System.Collections;

using LibMyWorkout.Domain;
using myworkout.model.service.getExercisesService;
using myworkout.model.service.completeDayService;
using myworkout.model.service.exceptions.InvalidUserException;

namespace myworkout.model.business
{
	public class WorkoutMgr : Manager
	{

		public IList<Exercise> getExercises(User user)
		{
			IList<Exercise> exerciseList = null;
			try
			{
				IGetExercisesService getExerciseService = (IGetExercisesService)GetService(typeof(IGetExercisesService).Name);
				exerciseList = getExerciseService.getExercises(user);
			}
			catch (InvalidUserException)
			{
			}

			return exerciseList;
		}



		public User completeDay(User u, Hashtable ht)
		{
			User user = null;
			try
			{
				ICompleteDayService completeDayService = (ICompleteDayService)GetService(typeof(ICompleteDayService).Name);
				user = completeDayService.completeDay(u, ht);
			}
			catch (InvalidUserException){}


			return user;
		}



	}
}
