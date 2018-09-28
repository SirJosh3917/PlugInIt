using PlugInIt;
using System;
using System.Threading;
using TestProject.Core;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
			foreach(var i in PluginLoader.LoadPluginsFromDirectory<IPlugin>("plugins")) {
				i.Start();

				i.Stop();
			}

			Console.ReadLine();
        }
    }
}
