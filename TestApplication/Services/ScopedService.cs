using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Core.BootStrap;

namespace TestApplication.Services
{
	public class ScopedService: IScopedService, IScopedDependency
	{
		public int counter { get; set; }

		public string getMessage()
		{
			counter++;
			return String.Concat("Counter Value Scoped ", counter);
		}
	}
}
