using System;
using System.Collections.Generic;
using System.Linq;

namespace PlugInIt {
	public class PluginManager<T> : IDisposable
		where T : IManagedPlugin {
		public PluginManager(IEnumerable<T> plugins) {
			var pluginList = new List<T>();

			foreach (var i in plugins)
				pluginList.Add(i);

			this._plugins = pluginList.OrderByDescending(x => x.Priority).ToArray();
			this._enabledStates = new bool[this._plugins.Length];
		}

		private T[] _plugins;
		private bool[] _enabledStates;

		public void StartAll() {
			while(StartNext()) { }
		}

		public void StopAll() {
			while(StopNext()) { }
		}

		public bool StartNext() {
			for(int i = 0; i < this._enabledStates.Length; i++)
				if(!this._enabledStates[i]) {
					this._plugins[i].Start();
					return true;
				}

			return false;
		}

		public bool StopNext() {
			for (int i = 0; i < this._enabledStates.Length; i++)
				if (this._enabledStates[i]) {
					this._plugins[i].Stop();
					return true;
				}

			return false;
		}

		public void Dispose() {
			StopAll();
		}
	}
}