using System;

using myworkout.model.service.factory;
using myworkout.model.service;

namespace myworkout.model.business
{
	public abstract class Manager
	{
		private Factory factory = Factory.GetInstance();

		protected IService GetService(string name)
		{
			return factory.getService(name);
		}

	}
}
