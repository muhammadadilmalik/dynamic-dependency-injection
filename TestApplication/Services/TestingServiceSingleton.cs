using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Core.BootStrap;

namespace TestApplication.Services
{
	public class TestingServiceSingleton: ITestingServiceSingleton, ISingletonDependency
	{
		public int counter { get; set; }

		public string getMessage()
		{
			counter++;
			return String.Concat("Counter Value Singleton ", counter);
		}
	}
}
