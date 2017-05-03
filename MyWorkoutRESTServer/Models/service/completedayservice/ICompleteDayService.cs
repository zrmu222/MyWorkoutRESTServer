using System;
using System.Collections;

using LibMyWorkout.Domain;

namespace myworkout.model.service.completeDayService
{
	public interface ICompleteDayService : IService
	{
		User completeDay(User user, Hashtable hashTable);
	}
}
