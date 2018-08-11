using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PlugInIt {
	public static class PluginLoader {
		public static IEnumerable<T> LoadPluginsFromDirectories<T>(params string[] dirs)
			where T : IPlugin {
			foreach (var i in dirs)
				foreach (var j in LoadPluginsFromDirectory<T>(Path.GetFullPath(i)))
					yield return j;
		}

		public static IEnumerable<T> LoadPluginsFromDirectory<T>(string dir)
			where T : IPlugin {
			if (!EnsureExists(dir)) yield break;

			var paths = GetFiles(dir);

			foreach (var j in paths)
				foreach (var k in LoadPluginsByFileName<T>(Path.GetFullPath(j)))
					yield return k;
		}

		public static IEnumerable<T> LoadPluginsByFileName<T>(params string[] dlls)
			where T : IPlugin {
			foreach (var i in dlls)
				foreach (var j in LoadPluginByFileName<T>(Path.GetFullPath(i)))
					yield return j;
		}

		public static IEnumerable<T> LoadPluginByFileName<T>(string dllPath)
			where T : IPlugin {
			var dll = LoadAssembly(dllPath);

			if (dll == null) yield break;

			Type[] types;

			try {
				types = dll.GetExportedTypes();
			} catch (Exception) { yield break; }

			foreach (var type in types) {
				Type[] interfaces;

				try { interfaces = type.GetInterfaces(); } catch (Exception) { continue; }

				if (interfaces.Contains(typeof(T))) {
					T res;

					try {
						res = (T)Activator.CreateInstance(type);
					} catch (Exception) { continue; }

					yield return res;
				}
			}
		}

		// try catches - a necessary evil

		private static bool EnsureExists(string dir) {
			var path = Path.GetFullPath(dir);

			if (!Directory.Exists(path))
				try {
					Directory.CreateDirectory(path);
				} catch (Exception) { return false; }

			return true;
		}

		private static string[] GetFiles(string dir) {
			var path = Path.GetFullPath(dir);

			try {
				return Directory.GetFiles(dir);
			} catch (Exception) { return Array.Empty<string>(); }
		}

		private static Assembly LoadAssembly(string file) {
			try {
				return Assembly.LoadFile(Path.GetFullPath(file));
			} catch (Exception) { return null; }
		}
	}
}