using System;

namespace PlugInIt {
	public interface IPlugin {

	}

	public interface IManagedPlugin : IPlugin {
		ushort Priority { get; }

		void Start();
		void Stop();
	}
}