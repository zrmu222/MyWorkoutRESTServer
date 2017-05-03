using System;
using System.Collections.Generic;

using LibMyWorkout.Domain;

namespace myworkout.model.service.getExercisesService
{
	public interface IGetExercisesService : IService
	{

		IList<Exercise> getExercises(User user);

	}
}
