using System;

namespace TestProject.Core {

	public interface IPlugin {
		string Author { get; }
		string Version { get; }
		Guid Id { get; }

		void Start();
		void Stop();
	}
}
