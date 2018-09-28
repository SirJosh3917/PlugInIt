using System;
using TestProject.Core;

namespace TestPlugin {

	public class MyMoreImportantPlugin : IPlugin {
		public string Author => "The Author";
		public string Version => "v0.0.1";

		public Guid Id => new Guid("1513437f-9db7-488a-b78d-3c35c0c5eff3");

		public ushort Priority => 10;

		public void Start() => Console.WriteLine("Important Plugin Started!");
		public void Stop() => Console.WriteLine("Important Plugin Stopped!");
	}

	public class MyPlugin : IPlugin {
		public string Author => "The Author";
		public string Version => "v0.0.1";

		public Guid Id => new Guid("1413437f-9db7-488a-b78d-3c35c0c5eff3");

		public ushort Priority => 1;

		public void Start() => Console.WriteLine("Plugin Started!");
		public void Stop() => Console.WriteLine("Plugin Stopped!");
	}
}
